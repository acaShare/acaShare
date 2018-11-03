using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models.StructureManagement;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.StructureManagement
{
    [Authorize]
    [Area("Moderator")]
    public class UniversitiesManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public UniversitiesManagementController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult Universities()
        {
            var universities = _service.GetUniversities();

            var universityViewModels = universities.Select(u =>
                new UniversityViewModel
                {
                    Id = u.UniversityId,
                    TitleOrFullName = u.Name,
                }
            ).ToList();
            
            var vm = new ListViewModel<UniversityViewModel>
            {
                Items = universityViewModels,
                IsWithSubtitles = false
            };

            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UniversityViewModel university)
        {
            return RedirectToAction("Universities");
        }

        public IActionResult Edit(int universityId)
        {
            var universityToEdit = _service.GetUniversity(universityId);

            var vm = new UniversityViewModel
            {
                TitleOrFullName = universityToEdit.Name,
                SubtitleOrAbbreviation = universityToEdit.Name
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(UniversityViewModel universityToUpdate)
        {
            return RedirectToAction("Universities");
        }

        public IActionResult Delete(int universityId, bool? confirmation)
        {
            if(!confirmation.HasValue)
            {
                var universityToDelete = _service.GetUniversity(universityId);

                var vm = new UniversityViewModel
                {
                    Id = universityId,
                    TitleOrFullName = universityToDelete.Name
                };

                return View(vm);
            }
            else if (confirmation.Value == true)
            {
                // actually delete
                return RedirectToAction("Universities");
            }

            return RedirectToAction("Universities");
        }
    }
}