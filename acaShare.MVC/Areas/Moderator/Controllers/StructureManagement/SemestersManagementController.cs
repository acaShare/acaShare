using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models.StructureManagement;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.StructureManagement
{
    [Authorize]
    [Area("Moderator")]
    public class SemestersManagementController : Controller
    {
        private readonly IUniversityTreeTraversalService _traversalService;

        public SemestersManagementController(IUniversityTreeTraversalService traversalService)
        {
            _traversalService = traversalService;
        }

        public IActionResult Semesters(int departmentId)
        {
            ConfigureBreadcrumbs(departmentId);

            var semesters = _traversalService.GetSemesters();

            var semesterViewModels = 
                semesters.Select(s =>
                    new SemesterViewModel
                    {
                        Id = s.SemesterId,
                        TitleOrFullName = s.Number
                    })
                .OrderBy(s => s.Id)
                .ToList();

            var vm = new ListViewModel<SemesterViewModel>
            {
                Items = semesterViewModels
            };

            return View(vm);
        }

        private void ConfigureBreadcrumbs(int departmentId)
        {
            var department = _traversalService.GetDepartment(departmentId);
            var university = _traversalService.GetUniversity(department.University.UniversityId);

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
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
                },
                new Breadcrumb
                {
                    Controller = "SemestersManagement",
                    Action = "Semesters",
                    Title = department.Abbreviation,
                    Params = parms
                }
            };
        }
    }
}