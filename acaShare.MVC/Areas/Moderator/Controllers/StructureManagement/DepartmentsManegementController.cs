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
            var university = _traversalService.GetUniversity(universityId);
            if (university == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            ConfigureListBreadcrumbs(university);

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


        public IActionResult Add(int universityId)
        {
            var university = _traversalService.GetUniversity(universityId);
            if (university == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            ConfigureAddBreadcrumbs(university);

            var vm = new DepartmentViewModel
            {
                UniversityId = universityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var university = _traversalService.GetUniversity(vm.UniversityId);
            if (university == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

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
            if (departmentToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o podanym Id nie istnieje." });
            }

            ConfigureEditBreadcrumbs(departmentToEdit);

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
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var university = _traversalService.GetUniversity(vm.UniversityId);
            if (university == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            var departmentToEdit = _traversalService.GetDepartment(vm.Id);
            if (departmentToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o podanym Id nie istnieje." });
            }

            departmentToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation, university);

            bool success = _managementService.UpdateDepartment(departmentToEdit);

            if (!success)
            {
                ModelState.AddModelError("ERROR", "Wydział o takiej nazwie lub skrócie istnieje już na tej uczelni");
                return View(vm);
            }

            return RedirectToAction("Departments", new { universityId = vm.UniversityId });
        }


        public IActionResult Delete(int departmentId, bool confirmation = false)
        {
            var departmentToDelete = _traversalService.GetDepartment(departmentId);
            if (departmentToDelete == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o podanym Id nie istnieje." });
            }

            ConfigureDeleteBreadcrumbs(departmentToDelete);

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
                foreach (var lesson in departmentToDelete.Lessons)
                {
                    foreach (var materialToDelete in lesson.Materials)
                    {
                        _filesManagement.DeleteWholeMaterialFolder(materialToDelete.MaterialId);
                        _materialsService.DeleteMaterial(materialToDelete);
                    }
                }
                
                // actually delete
                _managementService.DeleteDepartment(departmentToDelete);

                return RedirectToAction("Departments", new { universityId = departmentToDelete.UniversityId });
            }
        }

        #region breadcrumbs
        private void ConfigureListBreadcrumbs(BLL.Models.University university)
        {
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
                    Params = new Dictionary<string, string>
                    {
                        { "universityId", university.UniversityId.ToString() }
                    }
                }
            };
        }

        private void ConfigureAddBreadcrumbs(BLL.Models.University university)
        {
            ConfigureListBreadcrumbs(university);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "DepartmentsManagement",
                    Action = "Add",
                    Title = "Dodawanie wydziału",
                    Params = new Dictionary<string, string>
                    {
                        { "universityId", university.UniversityId.ToString() }
                    }
                }
            );
        }

        private void ConfigureEditBreadcrumbs(BLL.Models.Department department)
        {
            ConfigureListBreadcrumbs(department.University);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "DepartmentsManagement",
                    Action = "Edit",
                    Title = "Edycja wydziału",
                    Params = new Dictionary<string, string>
                    {
                        { "departmentId", department.DepartmentId.ToString() }
                    }
                }
            );
        }

        private void ConfigureDeleteBreadcrumbs(BLL.Models.Department department)
        {
            ConfigureListBreadcrumbs(department.University);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "DepartmentsManagement",
                    Action = "Delete",
                    Title = "Usuwanie wydziału",
                    Params = new Dictionary<string, string>
                    {
                        { "departmentId", department.DepartmentId.ToString() }
                    }
                }
            );
        }
        #endregion
    }
}