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
    [Authorize]
    [Area("Moderator")]
    public class LessonsManagementController : Controller
    {
        /// <summary>
        /// The controller's CUD methods manage subjects in department, but show Lessons. It is a side effect of our db archritecture.
        /// In fact, Lesson IS SubjectDepartment in a given semester
        /// </summary>
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;

        public LessonsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService)
        {
            _traversalService = traversalService;
            _managementService = managementService;
        }

        public IActionResult Lessons(int semesterId, int departmentId)
        {
            ConfigureBreadcrumbs(semesterId, departmentId);

            var subjectDepartmentAssociationResults = _traversalService.GetSubjectDepartmentAssociationResultsForDepartment(departmentId);

            var lessons = _traversalService.GetLessons(semesterId, subjectDepartmentAssociationResults);

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

        private void ConfigureBreadcrumbs(int semesterId, int departmentId)
        {
            var department = _traversalService.GetDepartment(departmentId);
            var university = department.University;
            var semester = _traversalService.GetSemester(semesterId);

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
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Lessons",
                    Title = semester.Number,
                    Params = parms
                }
            };
        }
        private void ConfigureAddBreadcrumbs(int semesterId, int departmentId)
        {
            var department = _traversalService.GetDepartment(departmentId);
            var university = department.University;
            var semester = _traversalService.GetSemester(semesterId);

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
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Lessons",
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Add",
                    Title = "Dodawanie przedmiotu",
                    Params = parms
                }
            };
        }
        private void ConfigureEditBreadcrumbs(BLL.Models.Lesson lesson)
        {
            var department = lesson.SubjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() },
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
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Lessons",
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Edit",
                    Title = "Edycja przedmiotu",
                    Params = parms
                }
            };
        }
        private void ConfigureDeleteBreadcrumbs(BLL.Models.Lesson lesson)
        {
            var department = lesson.SubjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() },
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
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Lessons",
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Delete",
                    Title = "Usuwanie przedmiotu",
                    Params = parms
                }
            };
        }


        // Create new subject and add it to the department
        public IActionResult Add(int semesterId, int departmentId)
        {
            ConfigureAddBreadcrumbs(semesterId, departmentId);

            var vm = new SubjectDepartmentViewModel // treat this as subject view model
            {
                SemesterId = semesterId,
                DepartmentId = departmentId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(SubjectDepartmentViewModel vm)
        {
            var department = _traversalService.GetDepartment(vm.DepartmentId);

            var subjectToAdd = new BLL.Models.Subject(vm.TitleOrFullName, vm.SubtitleOrAbbreviation, department);

            var addedSubject = _managementService.AddSubject(subjectToAdd);

            var subjectDepartmentId = addedSubject.SubjectDepartment.Max(s => s.SubjectDepartmentId);

            var lesson = new BLL.Models.Lesson(vm.SemesterId, subjectDepartmentId);

            _managementService.AddLesson(lesson);

            return RedirectToAction("Lessons", new { semesterId = vm.SemesterId, departmentId = department.DepartmentId });
        }


        public IActionResult Edit(int lessonId)
        {
            var lessonToEdit = _traversalService.GetLesson(lessonId);

            ConfigureEditBreadcrumbs(lessonToEdit);

            var vm = new LessonViewModel
            {
                Id = lessonToEdit.LessonId,
                TitleOrFullName = lessonToEdit.SubjectDepartment.Subject.Name,
                SubtitleOrAbbreviation = lessonToEdit.SubjectDepartment.Subject.Abbreviation,
                SemesterId = lessonToEdit.SemesterId,
                DepartmentId = lessonToEdit.SubjectDepartment.DepartmentId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(LessonViewModel vm)
        {
            var lessonToEdit = _traversalService.GetLesson(vm.Id);
            lessonToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            _managementService.UpdateLesson(lessonToEdit);

            return RedirectToAction("Lessons", new { semesterId = vm.SemesterId, departmentId = vm.DepartmentId });
        }


        public IActionResult Delete(int lessonId, bool confirmation = false)
        {
            var lessonToDelete = _traversalService.GetLesson(lessonId);

            ConfigureDeleteBreadcrumbs(lessonToDelete);

            if (!confirmation)
            {
                var vm = new LessonViewModel
                {
                    Id = lessonId,
                    TitleOrFullName = lessonToDelete.SubjectDepartment.Subject.Name,
                    SemesterId = lessonToDelete.SemesterId,
                    DepartmentId = lessonToDelete.SubjectDepartment.DepartmentId,
                    MaterialsCount = lessonToDelete.MaterialsCount
                };

                return View(vm);
            }
            else
            {
                // actually delete
                _managementService.DeleteLesson(lessonToDelete.LessonId);

                return RedirectToAction("Lessons", new { semesterId = lessonToDelete.SemesterId, departmentId = lessonToDelete.SubjectDepartment.DepartmentId });
            }
        }
    }
}