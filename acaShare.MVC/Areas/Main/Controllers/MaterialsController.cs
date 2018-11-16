using System;
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
            ConfigureMaterialsBreadcrumbs(lessonId);

            var lesson = _traversalService.GetLesson(lessonId);

            var materials = lesson.Materials;

            var materialViewModels = materials.Select(m =>
                new MaterialViewModel
                {
                    MaterialId = m.MaterialId,
                    Creator = new UserViewModel { UserId = m.CreatorId, Username = m.Creator?.Username, IdentityUserId = m.Creator.IdentityUserId },
                    Approver = new UserViewModel { UserId = m.ApproverId ?? -1, Username = m.Approver?.Username, IdentityUserId = m.Approver?.IdentityUserId },
                    Updater = new UserViewModel { UserId = m.UpdaterId ?? -1, Username = m.Updater?.Username, IdentityUserId = m.Updater?.IdentityUserId },
                    Lesson = new LessonViewModel { LessonId = lesson.LessonId, SemesterId = lesson.SemesterId, SubjectDepartmentId = lesson.SubjectDepartmentId },
                    Name = m.Name,
                    Description = m.Description,
                    UploadDate = m.UploadDate,
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

        private void ConfigureMaterialsBreadcrumbs(int lessonId)
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
                }
            };
        }


        public IActionResult Material(int materialId)
        {
            ConfigureMaterialBreadcrumbs(materialId);

            var material = _service.GetMaterial(materialId);
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isFavorite = loggedUser.IsMaterialFavorite(material);
            
            var vm = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                Creator = new UserViewModel { UserId = material.CreatorId, Username = material.Creator.Username, IdentityUserId = material.Creator.IdentityUserId },
                Approver = new UserViewModel { UserId = material.ApproverId ?? -1, Username = material.Approver?.Username, IdentityUserId = material.Approver?.IdentityUserId },
                Updater = new UserViewModel { UserId = material.UpdaterId ?? -1, Username = material.Updater?.Username, IdentityUserId = material.Updater?.IdentityUserId },
                Lesson = new LessonViewModel
                {
                    LessonId = material.Lesson.LessonId,
                    SemesterId = material.Lesson.SemesterId,
                    SubjectDepartmentId = material.Lesson.SubjectDepartmentId
                },
                Name = material.Name,
                Description = material.Description,
                UploadDate = material.UploadDate,
                ModificationDate = material.ModificationDate,
                State = material.State.Name,
                IsFavorite = isFavorite
            };

            return View(vm);
        }

        private void ConfigureMaterialBreadcrumbs(int materialId)
        {
            var material = _service.GetMaterial(materialId);
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

        public IActionResult AddComment(string newComment, int materialId)
        {
            var commentAuthor = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var material = _service.GetMaterial(materialId);

            _service.AddComment(newComment, material, commentAuthor);

            return RedirectToAction("Material", new { @materialId = materialId });
        }
    }
}