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
                new DepartmentViewModel
                {
                    Id = d.DepartmentId,
                    TitleOrFullName = d.Name,
                    UniversityId = universityId
                }
            ).ToList();

            var vm = new ListViewModel<DepartmentViewModel>
            {
                Items = departmentViewModels
            };

            return View(vm);
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
        public IActionResult Add(DepartmentViewModel departmentToAdd)
        {
            return RedirectToAction("Departments", new { universityId = departmentToAdd.UniversityId });
        }

        public IActionResult Edit(int departmentId)
        {
            var departmentToEdit = _service.GetDepartment(departmentId);

            var vm = new DepartmentViewModel
            {
                TitleOrFullName = departmentToEdit.Name,
                UniversityId = departmentToEdit.UniversityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel departmentToUpdate)
        {
            return RedirectToAction("Departments", new { universityId = departmentToUpdate.UniversityId });
        }

        public IActionResult Delete(int departmentId, bool confirmation = false)
        {
            var departmentToDelete = _service.GetDepartment(departmentId);

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
                // actually delete
                return RedirectToAction("Departments", new { universityId = departmentToDelete.UniversityId });
            }
        }
    }
}