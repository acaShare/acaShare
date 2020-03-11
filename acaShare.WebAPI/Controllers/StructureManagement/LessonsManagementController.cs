using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
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
    public class LessonsManagementController : Controller
    {
        /// <summary>
        /// The controller's CUD methods manage subjects in department, but show Lessons. It is a side effect of our db archritecture.
        /// In fact, Lesson IS SubjectDepartment in a given semester
        /// </summary>
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUniversityTreeManagementService _managementService;
        private readonly IMaterialsService _materialsService;
        private readonly IFormFilesManagement _filesManagement;

        public LessonsManagementController(IUniversityTreeTraversalService traversalService, IUniversityTreeManagementService managementService,
            IMaterialsService materialsService, IFormFilesManagement formFilesManagement)
        {
            _traversalService = traversalService;
            _managementService = managementService;
            _materialsService = materialsService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult Lessons(int semesterId, int departmentId)
        {
            var semester = _traversalService.GetSemester(semesterId);
            if (semester == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "semestr o takim Id nie istnieje." });
            }

            var department = _traversalService.GetDepartment(departmentId);
            if (department == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o takim Id nie istnieje." });
            }

            ConfigureListBreadcrumbs(semester, department, department.University);

            var lessons = _traversalService.GetLessons(semester, department);

            var viewModels = lessons.Select(l =>
                new LessonViewModel
                {
                    Id = l.LessonId,
                    TitleOrFullName = l.Subject.Name,
                    SubtitleOrAbbreviation = l.Subject.Abbreviation
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

        // Create new subject and add it to the department
        public IActionResult Add(int semesterId, int departmentId)
        {
            var semester = _traversalService.GetSemester(semesterId);
            if (semester == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "semestr o takim Id nie istnieje." });
            }

            var department = _traversalService.GetDepartment(departmentId);
            if (department == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o takim Id nie istnieje." });
            }

            ConfigureAddBreadcrumbs(semester, department, department.University);

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
            var semester = _traversalService.GetSemester(vm.SemesterId);
            if (semester == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "semestr o takim Id nie istnieje." });
            }

            var department = _traversalService.GetDepartment(vm.DepartmentId);
            if (department == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "wydział o takim Id nie istnieje." });
            }

            var lesson = new BLL.Models.Lesson(vm.SemesterId, department, vm.TitleOrFullName, vm.SubtitleOrAbbreviation);
            var success = _managementService.AddLesson(lesson);

            if (!success)
            {
                ModelState.AddModelError("ERROR", "Przedmiot o takiej nazwie lub skrócie istnieje już na tym wydziale w tym semestrze");
                return View(vm);
            }

            return RedirectToAction("Lessons", new { semesterId = vm.SemesterId, departmentId = department.DepartmentId });
        }


        public IActionResult Edit(int lessonId)
        {
            var lessonToEdit = _traversalService.GetLesson(lessonId);
            if (lessonToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "przedmiot o takim Id nie istnieje." });
            }

            ConfigureEditBreadcrumbs(lessonToEdit);

            var vm = new LessonViewModel
            {
                Id = lessonToEdit.LessonId,
                TitleOrFullName = lessonToEdit.Subject.Name,
                SubtitleOrAbbreviation = lessonToEdit.Subject.Abbreviation,
                SemesterId = lessonToEdit.SemesterId,
                DepartmentId = lessonToEdit.DepartmentId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(LessonViewModel vm)
        {
            var lessonToEdit = _traversalService.GetLesson(vm.Id);
            if (lessonToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "przedmiot o takim Id nie istnieje." });
            }

            bool success = _managementService.UpdateLesson(lessonToEdit.LessonId, vm.TitleOrFullName, vm.SubtitleOrAbbreviation);

            if (!success)
            {
                ModelState.AddModelError("ERROR", "Przedmiot o takiej nazwie lub skrócie istnieje już na tym wydziale w tym semestrze");
                return View(vm);
            }

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
                    TitleOrFullName = lessonToDelete.Subject.Name,
                    SemesterId = lessonToDelete.SemesterId,
                    DepartmentId = lessonToDelete.DepartmentId,
                    MaterialsCount = lessonToDelete.MaterialsCount
                };

                return View(vm);
            }
            else
            {
                // First - delete materials due to database constraints betwee Lesson and Material
                foreach (var material in lessonToDelete.Materials)
                {
                    _filesManagement.DeleteWholeMaterialFolder(material.MaterialId);
                    _materialsService.DeleteMaterial(material);
                }

                // actually delete
                _managementService.DeleteLesson(lessonToDelete.LessonId);

                return RedirectToAction("Lessons", new { semesterId = lessonToDelete.SemesterId, departmentId = lessonToDelete.DepartmentId });
            }
        }

        #region breadcrumbs
        private void ConfigureListBreadcrumbs(BLL.Models.Semester semester, BLL.Models.Department department, BLL.Models.University university)
        {
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
                        { "departmentId", department.DepartmentId.ToString() },
                    }
                },
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Lessons",
                    Title = semester.Number,
                    Params = new Dictionary<string, string>
                    {
                        { "semesterId", semester.SemesterId.ToString() },
                        { "departmentId", department.DepartmentId.ToString() }
                    }
                }
            };
        }

        private void ConfigureAddBreadcrumbs(BLL.Models.Semester semester, BLL.Models.Department department, BLL.Models.University university)
        {
            ConfigureListBreadcrumbs(semester, department, university);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Add",
                    Title = "Dodawanie przedmiotu",
                    Params = new Dictionary<string, string>
                    {
                        { "semesterId", semester.SemesterId.ToString() },
                        { "departmentId", department.DepartmentId.ToString() }
                    }
                }
            );
        }

        private void ConfigureEditBreadcrumbs(BLL.Models.Lesson lesson)
        {
            ConfigureListBreadcrumbs(lesson.Semester, lesson.Department, lesson.Department.University);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Edit",
                    Title = "Edycja przedmiotu",
                    Params = new Dictionary<string, string>
                    {
                        { "lessonId", lesson.LessonId.ToString() }
                    }
                }
            );
        }

        private void ConfigureDeleteBreadcrumbs(BLL.Models.Lesson lesson)
        {
            ConfigureListBreadcrumbs(lesson.Semester, lesson.Department, lesson.Department.University);

            ViewBag.Breadcrumbs.Add(
                new Breadcrumb
                {
                    Controller = "LessonsManagement",
                    Action = "Delete",
                    Title = "Usuwanie przedmiotu",
                    Params = new Dictionary<string, string>
                    {
                        { "lessonId", lesson.LessonId.ToString() }
                    }
                }
            );
        }
        #endregion
    }
}