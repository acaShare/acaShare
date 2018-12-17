using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Models;
using acaShare.MVC.Models.StructureTraversal;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.StructureManagement
{
    [Authorize(Roles = Roles.AdministratorRole)]
    [Area("Moderator")]
    public class UniversitiesManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public UniversitiesManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        public IActionResult Universities()
        {
            ConfigureBreadcrumbs();

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

        private void ConfigureBreadcrumbs()
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


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UniversityViewModel vm)
        {
            var universityToAdd = new BLL.Models.University(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            _managementService.AddUniversity(universityToAdd);

            return RedirectToAction("Universities");
        }


        public IActionResult Edit(int universityId)
        {
            var universityToEdit = _traversalService.GetUniversity(universityId);

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
            var universityToEdit = _traversalService.GetUniversity(vm.Id);
            universityToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            _managementService.UpdateUniversity(universityToEdit);

            return RedirectToAction("Universities");
        }


        public IActionResult Delete(int universityId, bool confirmation = false)
        {
            var universityToDelete = _traversalService.GetUniversity(universityId);

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
                // actually delete
                _managementService.DeleteUniversity(universityToDelete);

                return RedirectToAction("Universities");
            }
        }
    }
}