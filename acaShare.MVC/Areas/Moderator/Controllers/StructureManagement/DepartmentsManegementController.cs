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
    public class DepartmentsManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public DepartmentsManagementController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult Departments(int universityId)
        {
            var departments = _service.GetDepartmentsFromUniversity(universityId);

            var departmentViewModels = departments.Select(d =>
                new ListItemViewModel
                {
                    Id = d.DepartmentId,
                    Title = d.Name
                }
            ).ToList();

            var vm = new ListViewModel<ListItemViewModel>
            {
                Items = departmentViewModels
            };

            return View(vm);
        }

        public IActionResult Add(int universityId)
        {
            var vm = new DepartmentViewModel {
                UniversityId = universityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel department)
        {
            return RedirectToAction("Departments", new { universityId = department.UniversityId });
        }

        public IActionResult Edit(int departmentId)
        {
            var departmentToEdit = _service.GetDepartment(departmentId);

            var vm = new DepartmentViewModel
            {
                Name = departmentToEdit.Name
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return RedirectToAction("Departments");
        }

        public IActionResult Delete(int departmentId, bool? confirmation)
        {
            if (!confirmation.HasValue)
            {
                var departmentToDelete = _service.GetDepartment(departmentId);

                var vm = new DepartmentViewModel
                {
                    DepartmentId = departmentId,
                    Name = departmentToDelete.Name
                };

                return View(vm);
            }
            else if (confirmation.Value == true)
            {
                // actually delete
                return RedirectToAction("Departments");
            }

            return RedirectToAction("Departments");
        }
    }
}