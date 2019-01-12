using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Common;
using acaShare.MVC.Models;
using acaShare.MVC.Models.StructureTraversal;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.StructureManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
    [Area("Moderator")]
    public class DepartmentsManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;

        public DepartmentsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
            IMaterialsService materialsService, IFormFilesManagement formFilesManagement)
        {
            _traversalService = traversalService;
            _managementService = managementService;
            _materialsService = materialsService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult Departments(int universityId)
        {
            ConfigureBreadcrumbs(universityId);

            var departments = _traversalService.GetDepartmentsFromUniversity(universityId);

            var departmentViewModels = departments.Select(d =>
                new DepartmentViewModel
                {
                    Id = d.DepartmentId,
                    TitleOrFullName = d.Name,
                    SubtitleOrAbbreviation = d.Abbreviation
                }
            ).ToList();

            var vm = new ListViewModel<DepartmentViewModel>
            {
                Items = departmentViewModels,
                IsWithSubtitles = true,
                HelperId = universityId
            };

            return View(vm);
        }

        private void ConfigureBreadcrumbs(int universityId)
        {
            var university = _traversalService.GetUniversity(universityId);

            var parms = new Dictionary<string, string>
            {
                { "universityId", universityId.ToString() }
            };

            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "UniversitiesManagement",
                    Action = "Universities",
                    Title = "Uczelnie"
                },
                new Breadcrumb
                {
                    Controller = "DepartmentsManagement",
                    Action = "Departments",
                    Title = university.Abbreviation,
                    Params = parms
                }
            };
        }


        public IActionResult Add(int universityId)
        {
            var vm = new DepartmentViewModel
            {
                UniversityId = universityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(vm.UniversityId);
            
            var departmentToAdd = new BLL.Models.Department(vm.TitleOrFullName, vm.SubtitleOrAbbreviation, university);
            
            var success = _managementService.AddDepartment(departmentToAdd);

            if (!success)
            {
                ModelState.AddModelError("ERROR", "Wydział o takiej nazwie lub skrócie istnieje już na tej uczelni");
                return View(vm);
            }

            return RedirectToAction("Departments", new { universityId = vm.UniversityId });
        }


        public IActionResult Edit(int departmentId)
        {
            var departmentToEdit = _traversalService.GetDepartment(departmentId);

            var vm = new DepartmentViewModel
            {
                Id = departmentToEdit.DepartmentId,
                TitleOrFullName = departmentToEdit.Name,
                SubtitleOrAbbreviation = departmentToEdit.Abbreviation,
                UniversityId = departmentToEdit.UniversityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(vm.UniversityId);

            var departmentToEdit = _traversalService.GetDepartment(vm.Id);
            departmentToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation, university);

            _managementService.UpdateDepartment(departmentToEdit);

            return RedirectToAction("Departments", new { universityId = vm.UniversityId });
        }


        public IActionResult Delete(int departmentId, bool confirmation = false)
        {
            var departmentToDelete = _traversalService.GetDepartment(departmentId);

            if (!confirmation)
            {
                var vm = new DepartmentViewModel
                {
                    Id = departmentId,
                    TitleOrFullName = departmentToDelete.Name,
                    UniversityId = departmentToDelete.UniversityId
                };

                return View(vm);
            }
            else
            {
                // First - delete materials due to database constraints betwee Lesson and Material
                foreach (var sd in departmentToDelete.SubjectDepartment)
                {
                    foreach (var lesson in sd.Lessons)
                    {
                        foreach (var materialToDelete in lesson.Materials)
                        {
                            _filesManagement.DeleteWholeMaterialFolder(materialToDelete.MaterialId);
                            _materialsService.DeleteMaterial(materialToDelete);
                        }
                    }
                }

                // actually delete
                _managementService.DeleteDepartment(departmentToDelete);

                return RedirectToAction("Departments", new { universityId = departmentToDelete.UniversityId });
            }
        }
    }
}