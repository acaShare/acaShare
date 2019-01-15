using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using acaShare.MVC.Areas.Main.Models;
using Microsoft.AspNetCore.Authorization;
using acaShare.ServiceLayer.Interfaces;
using acaShare.MVC.Models.StructureTraversal;
using acaShare.MVC.Models;

namespace acaShare.MVC.Areas.Main.Controllers
{
    [Authorize]
    [Area("Main")]
    public class ListController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public ListController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult AvailableUniversities()
        {
            ConfigureUniversitiesBreadcrumbs();

            var universities = _service.GetUniversities();

            var universityViewModels = universities.Select(u =>
                new UniversityViewModel
                {
                    Id = u.UniversityId,
                    TitleOrFullName = u.Name,
                    SubtitleOrAbbreviation = u.Abbreviation
                }
            ).ToList();

            var vm = new ListViewModel<UniversityViewModel>
            {
                Items = universityViewModels,
                IsWithSubtitles = true
            };

            return View(vm);
        }

        private void ConfigureUniversitiesBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "AvailableUniversities",
                    Title = "Uczelnie"
                }
            };
        }


        public IActionResult Departments(int universityId)
        {
            ConfigureDepartmentsBreadcrumbs(universityId);

            var departments = _service.GetDepartmentsFromUniversity(universityId);

            var departmentViewModels = departments.Select(d =>
                new DepartmentViewModel
                {
                    Id = d.DepartmentId,
                    TitleOrFullName = d.Name,
                    SubtitleOrAbbreviation = d.Abbreviation
                }
            ).ToList();

            var vm = new ListViewModel<DepartmentViewModel>
            {
                Items = departmentViewModels,
                IsWithSubtitles = true,
                HelperId = universityId
            };

            return View(vm);
        }

        private void ConfigureDepartmentsBreadcrumbs(int universityId)
        {
            var university = _service.GetUniversity(universityId);

            var parms = new Dictionary<string, string>
            {
                { "universityId", universityId.ToString() }
            };

            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "AvailableUniversities",
                    Title = "Uczelnie"
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Departments",
                    Title = university.Abbreviation + " - Wydziały",
                    Params = parms
                }
            };
        }


        public IActionResult Semesters(int departmentId)
        {
            ConfigureSemestersBreadcrumbs(departmentId);

            var semesters = _service.GetSemesters();

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

        private void ConfigureSemestersBreadcrumbs(int departmentId)
        {
            var department = _service.GetDepartment(departmentId);
            var university = _service.GetUniversity(department.University.UniversityId);

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
            };

            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "AvailableUniversities",
                    Title = "Uczelnie"
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Departments",
                    Title = university.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Semesters",
                    Title = department.Abbreviation + " - Semestry",
                    Params = parms
                }
            };
        }


        public IActionResult Lessons(int semesterId, int departmentId)
        {
            ConfigureLessonsBreadcrumbs(semesterId, departmentId);

            var subjectDepartmentAssociationResults = _service.GetSubjectDepartmentAssociationResultsForDepartment(departmentId);

            var lessons = _service.GetLessons(semesterId, subjectDepartmentAssociationResults);

            var viewModels = lessons.Select(l =>
                new LessonViewModel
                {
                    Id = l.LessonId,
                    TitleOrFullName = l.SubjectDepartment.Subject.Name,
                    SubtitleOrAbbreviation = l.SubjectDepartment.Subject.Abbreviation
                }
            ).ToList();

            var vm = new LessonsListViewModel
            {
                Items = viewModels,
                IsWithSubtitles = true,
                DepartmentId = departmentId,
                SemesterId = semesterId
            };

            return View(vm);
        }

        private void ConfigureLessonsBreadcrumbs(int semesterId, int departmentId)
        {
            var department = _service.GetDepartment(departmentId);
            var university = _service.GetUniversity(department.University.UniversityId);
            var semester = _service.GetSemester(semesterId);

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
            };

            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "AvailableUniversities",
                    Title = "Uczelnie"
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Departments",
                    Title = university.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Semesters",
                    Title = department.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "List",
                    Action = "Lessons",
                    Title = semester.Number + " - Przedmioty",
                    Params = parms
                }
            };
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
