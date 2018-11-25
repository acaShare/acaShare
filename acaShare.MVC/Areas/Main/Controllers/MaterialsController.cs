﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Main.Models.Materials;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Main.Controllers
{
    [Authorize]
    [Area("Main")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _service;
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUserService _userService;

        public MaterialsController(IMaterialsService service, IUniversityTreeTraversalService traversalService, IUserService userService)
        {
            _service = service;
            _traversalService = traversalService;
            _userService = userService;
        }

        public IActionResult Materials(int lessonId)
        {
            var lesson = _traversalService.GetLesson(lessonId);

            ConfigureMaterialsBreadcrumbs(lesson);
            
            var materials = lesson.Materials;

            var materialViewModels = materials.Select(m =>
                new MaterialViewModel
                {
                    MaterialId = m.MaterialId,
                    Name = m.Name,
                    Description = m.Description,
                    CreatorUsername = m.Creator.Username,
                    UploadDate = m.UploadDate,
                    UpdaterUsername = m.Updater?.Username,
                    ModificationDate = m.ModificationDate,
                    State = m.State.Name
                }
            ).ToList();

            var vms = new MaterialsViewModel
            {
                Materials = materialViewModels,
                IsWithSubtitles = true,
                LessonId = lessonId
            };

            return View(vms);
        }


        public IActionResult Material(int materialId)
        {
            var material = _service.GetMaterial(materialId);
            ConfigureMaterialBreadcrumbs(material);

            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isFavorite = loggedUser.IsMaterialFavorite(material);
            var isAllowedToEditOrDelete = material.IsUserAllowedToEditOrDelete(loggedUser);

            var vm = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                CreatorUsername = material.Creator.Username,
                ApproverUsername = material.Approver?.Username,
                UpdaterUsername = material.Updater?.Username,
                Name = material.Name,
                Description = material.Description,
                UploadDate = material.UploadDate,
                ModificationDate = material.ModificationDate,
                State = material.State.Name,
                IsFavorite = isFavorite,
                IsAllowedToEditOrDelete = isAllowedToEditOrDelete
            };

            return View(vm);
        }


        public IActionResult Add(int lessonId)
        {
            ConfigureAddMaterialBreadcrumbs(lessonId);

            var vm = new AddMaterialViewModel
            {
                LessonId = lessonId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(AddMaterialViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lesson = _traversalService.GetLesson(vm.LessonId);
            var creator = _userService.FindByIdentityUserId(identityUserId);
            var state = _service.GetState(MaterialStateEnum.PENDING);

            var materialToAdd = new BLL.Models.Material(vm.Name, vm.Description, lesson, creator, state);

            if (vm.Files != null && vm.Files.ToList().Count != 0)
            {
                foreach (var fileData in vm.Files)
                {
                    var file = new BLL.Models.File(fileData);
                    materialToAdd.AddFile(file);
                }
            }

            _service.AddMaterial(materialToAdd);

            return RedirectToAction("Materials", new { LessonId = lesson.LessonId });
        }


        public IActionResult Edit(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(materialId);

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return NotFound("Nie masz uprawnień do tego działania"); // TODO zrobić jakiś handler do tego w stylu 404 not found
            }
            
            ConfigureEditMaterialBreadcrumbs(materialToEdit.Lesson);

            var vm = new EditMaterialViewModel
            {
                MaterialId = materialId,
                Name = materialToEdit.Name,
                Description = materialToEdit.Description,
                Files = materialToEdit.Files.Select(f => f.File1).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditMaterialViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(vm.MaterialId);

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return NotFound("Nie masz uprawnień do tego działania"); // zrobić jakiś handler do tego
            }

            var loggedUser = _userService.FindByIdentityUserId(identityUserId);
            var filesToRemove = materialToEdit.Update(vm.Name, vm.Description, vm.Files, loggedUser);

            _service.UpdateMaterial(materialToEdit, filesToRemove);

            return RedirectToAction("Material", new { @materialId = vm.MaterialId });
        }


        public IActionResult Delete(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToDelete = _service.GetMaterial(materialId);

            if (identityUserId != materialToDelete.Creator.IdentityUserId)
            {
                return NotFound("Nie masz uprawnień do tego działania"); // zrobić jakiś handler do tego
            }

            ConfigureDeleteMaterialBreadcrumbs(materialToDelete.Lesson);

            var vm = new DeleteMaterialViewModel
            {
                MaterialId = materialToDelete.MaterialId,
                Name = materialToDelete.Name,
                LessonId = materialToDelete.LessonId,
                FilesCount = materialToDelete.FilesCount
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(DeleteMaterialViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToDelete = _service.GetMaterial(vm.MaterialId);

            if (identityUserId != materialToDelete.Creator.IdentityUserId)
            {
                return NotFound("Nie masz uprawnień do tego działania"); // zrobić jakiś handler do tego
            }

            _service.DeleteMaterial(materialToDelete);

            return RedirectToAction("Materials", new { @lessonId = vm.LessonId });
        }


        #region breadcrumbs
        private void ConfigureMaterialsBreadcrumbs(BLL.Models.Lesson lesson)
        {
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() }
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
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Materials",
                    Title = subjectDepartment.Subject.Abbreviation,
                    Params = parms
                }
            };
        }

        private void ConfigureMaterialBreadcrumbs(BLL.Models.Material material)
        {
            var lesson = material.Lesson;
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() },
                { "materialId", material.MaterialId.ToString() }
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
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Materials",
                    Title = subjectDepartment.Subject.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Material",
                    Title = material.Name,
                    Params = parms
                }
            };
        }

        private void ConfigureAddMaterialBreadcrumbs(int lessonId)
        {
            var lesson = _traversalService.GetLesson(lessonId);
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() }
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
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Materials",
                    Title = subjectDepartment.Subject.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Add",
                    Title = "Dodawanie materiału",
                    Params = parms
                }
            };
        }

        private void ConfigureEditMaterialBreadcrumbs(BLL.Models.Lesson lesson)
        {
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() }
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
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Materials",
                    Title = subjectDepartment.Subject.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Add",
                    Title = "Edycja materiału",
                    Params = parms
                }
            };
        }

        private void ConfigureDeleteMaterialBreadcrumbs(BLL.Models.Lesson lesson)
        {
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;

            var parms = new Dictionary<string, string>
            {
                { "universityId", university.UniversityId.ToString() },
                { "departmentId", department.DepartmentId.ToString() },
                { "semesterId", semester.SemesterId.ToString() },
                { "lessonId", lesson.LessonId.ToString() }
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
                    Title = semester.Number,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Materials",
                    Title = subjectDepartment.Subject.Abbreviation,
                    Params = parms
                },
                new Breadcrumb
                {
                    Controller = "Materials",
                    Action = "Add",
                    Title = "Usuwanie materiału",
                    Params = parms
                }
            };
        }
        #endregion


        public IActionResult ToggleFavorites(int materialId)
        {
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var material = _service.GetMaterial(materialId);

            _service.ToggleFavorite(material, loggedUser);

            return RedirectToAction("Material", new { @materialId = materialId });
        }

        [HttpPost]
        public IActionResult AddComment(string newComment, int materialId)
        {
            var commentAuthor = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var material = _service.GetMaterial(materialId);

            _service.AddComment(newComment, material, commentAuthor);

            return RedirectToAction("Material", new { @materialId = materialId });
        }
    }
}