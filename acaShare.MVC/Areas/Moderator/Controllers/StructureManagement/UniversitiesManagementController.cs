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
    public class UniversitiesManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;

        public UniversitiesManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
            IMaterialsService materialsService, IFormFilesManagement formFilesManagement)
        {
            _traversalService = traversalService;
            _managementService = managementService;
            _materialsService = materialsService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult Universities()
        {
            ConfigureListBreadcrumbs();

            var universities = _traversalService.GetUniversities();

            var universityViewModels = universities.Select(u =>
                new UniversityViewModel
                {
                    Id = u.UniversityId,
                    TitleOrFullName = u.Name,
                    SubtitleOrAbbreviation = u.Abbreviation,
                }
            ).ToList();
            
            var vm = new ListViewModel<UniversityViewModel>
            {
                Items = universityViewModels,
                IsWithSubtitles = true
            };

            return View(vm);
        }

        private void ConfigureListBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "UniversitiesManagement",
                    Action = "Universities",
                    Title = "Uczelnie"
                }
            };
        }

        private void ConfigureAddBreadcrumbs()
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
                    Controller = "UniversitiesManagement",
                    Action = "Add",
                    Title = "Dodawanie uczelni"
                }
            };
        }

        private void ConfigureEditBreadcrumbs(int universityId)
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
                    Controller = "UniversitiesManagement",
                    Action = "Edit",
                    Title = "Edycja uczelni",
                    Params = new Dictionary<string, string>() { { "universityId", universityId.ToString() } }
                }
            };
        }

        private void ConfigureDeleteBreadcrumbs(int universityId)
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
                    Controller = "UniversitiesManagement",
                    Action = "Delete",
                    Title = "Usuwanie uczelni",
                    Params = new Dictionary<string, string>() { { "universityId", universityId.ToString() } }
                }
            };
        }

        public IActionResult Add()
        {
            ConfigureAddBreadcrumbs();
            return View();
        }

        [HttpPost]
        public IActionResult Add(UniversityViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var universityToAdd = new BLL.Models.University(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            var success = _managementService.AddUniversity(universityToAdd);

            if (!success)
            {
                ModelState.AddModelError("ERROR", "Uczelnia o takiej nazwie lub skrócie istnieje już w bazie");
                return View(vm);
            }

            return RedirectToAction("Universities");
        }


        public IActionResult Edit(int universityId)
        {
            var universityToEdit = _traversalService.GetUniversity(universityId);
            if (universityToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            ConfigureEditBreadcrumbs(universityId);
            
            var vm = new UniversityViewModel
            {
                Id = universityToEdit.UniversityId,
                TitleOrFullName = universityToEdit.Name,
                SubtitleOrAbbreviation = universityToEdit.Abbreviation
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(UniversityViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var universityToEdit = _traversalService.GetUniversity(vm.Id);
            if (universityToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            universityToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            _managementService.UpdateUniversity(universityToEdit);

            return RedirectToAction("Universities");
        }


        public IActionResult Delete(int universityId, bool confirmation = false)
        {
            var universityToDelete = _traversalService.GetUniversity(universityId);
            if (universityToDelete == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "uczelnia o podanym Id nie istnieje." });
            }

            ConfigureDeleteBreadcrumbs(universityId);

            if (!confirmation)
            {
                var vm = new UniversityViewModel
                {
                    Id = universityId,
                    TitleOrFullName = universityToDelete.Name
                };

                return View(vm);
            }
            else
            {
                // First - delete materials due to database constraints betwee Lesson and Material
                foreach (var dept in universityToDelete.Departments)
                {
                    foreach (var sd in dept.SubjectDepartment)
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
                }

                // actually delete
                _managementService.DeleteUniversity(universityToDelete);

                return RedirectToAction("Universities");
            }
        }
    }
}