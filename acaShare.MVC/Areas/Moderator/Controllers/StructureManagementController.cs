using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models.StructureManagement;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize]
    [Area("Moderator")]
    public class StructureManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public StructureManagementController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult Home()
        {
            var universities = _service.GetUniversities();

            var universityViewModels = universities.Select(u =>
                new ListItemViewModel
                {
                    Id = u.UniversityId,
                    Title = u.Name
                }
            ).ToList();

            var vm = new ListViewModel<ListItemViewModel>
            {
                Items = universityViewModels
            };

            return View(vm);
        }
    }
}