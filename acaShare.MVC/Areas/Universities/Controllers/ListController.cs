using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using acaShare.MVC.Areas.Universities.Models;
using Microsoft.AspNetCore.Authorization;
using acaShare.MVC.Areas.Universities.Models.Sidebar;
using acaShare.MVC.Areas.Universities.Models.List;
using acaShare.ServiceLayer.Interfaces;

namespace acaShare.MVC.Areas.Universities.Controllers
{
    [Authorize]
    [Area("Universities")]
    public class ListController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public ListController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult AvailableUniversities()
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


        public IActionResult Departments(int universityId)
        {
            var departments = _service.GetDepartmentsFromUniversity(universityId);

            var departmentViewModels = departments.Select(u =>
                new ListItemViewModel
                {
                    Id = u.UniversityId,
                    Title = u.Name
                }
            ).ToList();

            var vm = new ListViewModel<ListItemViewModel>
            {
                Items = departmentViewModels
            };

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
