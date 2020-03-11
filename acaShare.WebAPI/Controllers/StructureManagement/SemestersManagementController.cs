using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Models;
using acaShare.WebAPI.Models.StructureTraversal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.WebAPI.Controllers.StructureManagement
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole)]
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
            var department = _traversalService.GetDepartment(departmentId);
            if (department == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o podanym Id nie istnieje." });
            }

            ConfigureBreadcrumbs(department);

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
                Items = semesterViewModels,
                HelperId = departmentId
            };

            return View(vm);
        }

        private void ConfigureBreadcrumbs(BLL.Models.Department department)
        {
            var university = department.University;

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
                },
                new Breadcrumb
                {
                    Controller = "SemestersManagement",
                    Action = "Semesters",
                    Title = department.Abbreviation,
                    Params = new Dictionary<string, string>
                    {
                        { "departmentId", department.DepartmentId.ToString() }
                    }
                }
            };
        }
    }
}