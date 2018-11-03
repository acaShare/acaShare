﻿using System;
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
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public DepartmentsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        public IActionResult Departments(int universityId)
        {
            var departments = _traversalService.GetDepartmentsFromUniversity(universityId);

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
        public IActionResult Add(DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(vm.UniversityId);

            var departmentToAdd = new BLL.Models.Department(vm.TitleOrFullName, university);

            _managementService.AddDepartment(departmentToAdd);

            return RedirectToAction("Departments", new { universityId = vm.UniversityId });
        }


        public IActionResult Edit(int departmentId)
        {
            var departmentToEdit = _traversalService.GetDepartment(departmentId);

            var vm = new DepartmentViewModel
            {
                Id = departmentToEdit.DepartmentId,
                TitleOrFullName = departmentToEdit.Name,
                UniversityId = departmentToEdit.UniversityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(vm.UniversityId);

            var departmentToEdit = _traversalService.GetDepartment(vm.Id);
            departmentToEdit.Update(vm.TitleOrFullName, university);

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
                // actually delete
                _managementService.DeleteDepartment(departmentToDelete);

                return RedirectToAction("Departments", new { universityId = departmentToDelete.UniversityId });
            }
        }
    }
}