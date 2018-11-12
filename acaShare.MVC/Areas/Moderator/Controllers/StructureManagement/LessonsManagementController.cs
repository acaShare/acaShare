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
                    SubtitleOrAbbreviation = l.SubjectDepartment.Subject.Abbreviation,
                    //SemesterId = l.Semester.SemesterId,
                    //SubjectDepartmentId = l.SubjectDepartment.SubjectDepartmentId
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
            var university = _traversalService.GetUniversity(department.University.UniversityId);
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


        // Create new subject and add it to the department
        public IActionResult Add(int semesterId, int departmentId)
        {
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


        public IActionResult Edit(int departmentId)
        {
            var departmentToEdit = _traversalService.GetDepartment(departmentId);

            var vm = new DepartmentViewModel
            {
                Id = departmentToEdit.DepartmentId,
                TitleOrFullName = departmentToEdit.Name,
                SubtitleOrAbbreviation = departmentToEdit.Abbreviation,
                UniversityId = departmentToEdit.UniversityId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel vm)
        {
            var university = _traversalService.GetUniversity(vm.UniversityId);

            var departmentToEdit = _traversalService.GetDepartment(vm.Id);
            departmentToEdit.Update(vm.TitleOrFullName, vm.SubtitleOrAbbreviation, university);

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