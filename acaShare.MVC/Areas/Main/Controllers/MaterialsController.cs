using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using acaShare.MVC.Areas.Main.Models.Materials;
using acaShare.MVC.Common;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace acaShare.MVC.Areas.Main.Controllers
{
    [Authorize]
    [Area("Main")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _service;
        private readonly IUniversityTreeTraversalService _traversalService;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFormFilesManagement _filesManagement;
        private readonly IFilesValidator _filesValidator;

        public MaterialsController(IMaterialsService service, IUniversityTreeTraversalService traversalService, IUserService userService, 
            IHostingEnvironment hostingEnvironment, IFormFilesManagement formFilesManagement, IFilesValidator filesValidator)
        {
            _service = service;
            _traversalService = traversalService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _filesManagement = formFilesManagement;
            _filesValidator = filesValidator;
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

            var isFavorite = _userService.IsMaterialFavorite(material, User.FindFirstValue(ClaimTypes.NameIdentifier));

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
                Files = material.Files.Select(f => new FileViewModel
                {
                    FileName = f.FileName,
                    RelativePath = f.RelativePath,
                    ContentType = f.ContentType
                }).ToList(),
                IsFavorite = isFavorite,
                IsAllowedToEditOrDelete = material.IsUserAllowedToEditOrDelete(User.FindFirstValue(ClaimTypes.NameIdentifier)) // TODO Change to some authorization mechanism
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

        [ValidateMaterial(ViewModelName = "vm")]
        [HttpPost] // AJAX request
        public IActionResult Add(AddMaterialViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lesson = _traversalService.GetLesson(vm.LessonId);
            var creator = _userService.FindByIdentityUserId(identityUserId);
            var state = _service.GetState(MaterialStateEnum.PENDING);

            var materialToAdd = new BLL.Models.Material(vm.Name, vm.Description, lesson, creator, state);
            _service.AddMaterial(materialToAdd);

            var guid = Guid.NewGuid();

            try
            {
                _filesManagement.SaveFilesToFileSystem(vm.FormFiles, materialToAdd.MaterialId, guid);
            }
            catch (Exception ex)
            {
                return BadRequest("Somethig went wrong while saving files to the file system. Try again.");
            }

            // TODO splitted into two roundtrips to name folders with materialId (can be changed in the future)
            var filesToAdd = _filesManagement.ExtractFilesFromForm(vm.FormFiles, materialToAdd.MaterialId, guid);
            materialToAdd.AddFiles(filesToAdd);
            _service.UpdateMaterial(materialToAdd);

            return Json(materialToAdd.MaterialId);
        }


        public IActionResult Edit(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(materialId);

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return Forbid("Nie masz uprawnień do tego działania"); // TODO some authorization handler like 404 not found
            }
            
            ConfigureEditMaterialBreadcrumbs(materialToEdit.Lesson);

            var vm = new EditMaterialViewModel
            {
                MaterialId = materialId,
                Name = materialToEdit.Name,
                Description = materialToEdit.Description,
                Files = materialToEdit.Files.Select(f => new FileViewModel
                {
                    FileId = f.FileId,
                    FileName = f.FileName,
                    RelativePath = f.RelativePath,
                    ContentType = f.ContentType
                }).ToList()
            };

            return View(vm);
        }

        [ValidateMaterial(ViewModelName = "vm")]
        [HttpPost] // AJAX request
        public IActionResult Edit(EditMaterialViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(vm.MaterialId);

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return Forbid("Nie masz uprawnień do tego działania"); // TODO some authorization handler
            }

            var loggedUser = _userService.FindByIdentityUserId(identityUserId);

            if (vm.Files?.Count > 0)
            {
                var filesToRemove = materialToEdit.UpdateExistingFilesAndGetFilesToRemove(
                    vm.Files.Select(f => new BLL.Models.File(f.FileId, f.FileName)).ToList()
                );
                _filesManagement.RemoveFilesFromFileSystem(filesToRemove);
            }

            var guid = Guid.NewGuid();

            try
            {
                _filesManagement.SaveFilesToFileSystem(vm.FormFiles, vm.MaterialId, guid);
            }
            catch(Exception ex)
            {
                return BadRequest("Coś poszło nie tak przy zapisywaniu plików do systemu plików. Spróbuj ponownie.");
            }

            var newFiles = _filesManagement.ExtractFilesFromForm(vm.FormFiles, vm.MaterialId, guid);
            materialToEdit.Update(vm.Name, vm.Description, newFiles, loggedUser);
            _service.UpdateMaterial(materialToEdit);

            return Json(vm.MaterialId);
        }

        public IActionResult Delete(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToDelete = _service.GetMaterial(materialId);

            if (identityUserId != materialToDelete.Creator.IdentityUserId)
            {
                return Forbid("Nie masz uprawnień do tego działania"); // zrobić jakiś handler do tego
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
                return Forbid("Nie masz uprawnień do tego działania"); // zrobić jakiś handler do tego
            }

            _filesManagement.DeleteWholeMaterialFolder(materialToDelete.MaterialId);
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

        public IActionResult CreateDeleteSuggestion(int materialId, string materialName)
        {
            var reasons = _service.GetChangeReasons(BLL.Models.ChangeType.DELETE);

            var vm = new DeleteRequestViewModel
            {
                MaterialId = materialId,
                MaterialName = materialName,
                Reasons = reasons
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateDeleteSuggestion(DeleteRequestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var deleter = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                _service.CreateDeleteRequest(deleter, vm.MaterialId, vm.ReasonId, vm.AdditionalComment);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest("Materiał o podanym Id nie istnieje");
            }

            return RedirectToAction("Material", new { materialId = vm.MaterialId });
        }


        public IActionResult CreateEditSuggestion(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(materialId);

            if (identityUserId == materialToEdit.Creator.IdentityUserId)
            {
                return Forbid("Jesteś autorem danego materiału - skorzystaj z opcji edycji"); // TODO some authorization handler like 404 not found
            }

            ConfigureEditMaterialBreadcrumbs(materialToEdit.Lesson);

            var emvm = new EditMaterialViewModel
            {
                MaterialId = materialId,
                Name = materialToEdit.Name,
                Description = materialToEdit.Description
            };

            var vm = new EditRequestViewModel
            {
                EditMaterialViewModel = emvm,
                Files = materialToEdit.Files.Select(f => new FileViewModel
                {
                    FileId = f.FileId,
                    FileName = f.FileName,
                    RelativePath = f.RelativePath,
                    ContentType = f.ContentType
                }).ToList()
            };

            return View(vm);
        }

        
        [ValidateMaterial(ViewModelName = "vm")]
        [HttpPost] // AJAX request
        public IActionResult CreateEditSuggestion(EditRequestViewModel vm)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToEdit = _service.GetMaterial(vm.EditMaterialViewModel.MaterialId);
            
            if (identityUserId == materialToEdit.Creator.IdentityUserId)
            {
                return Forbid("Jesteś autorem danego materiału - skorzystaj z opcji edycji"); // TODO some authorization handler like 404 not found
            }

            var updater = _userService.FindByIdentityUserId(identityUserId);

            ICollection<BLL.Models.File> filesFromForm = new List<BLL.Models.File>();
            try
            {
                BLL.Models.EditRequest editRequest = _service.CreateEditRequest(
                       updater, materialToEdit, vm.Summary, vm.EditMaterialViewModel.Name, vm.EditMaterialViewModel.Description);

                var guid = Guid.NewGuid();

                // # physical save #
                _filesManagement.SaveFilesToFileSystem(vm.FormFiles, vm.EditMaterialViewModel.MaterialId, guid, editRequest.EditRequestId);

                // # database save #
                // existing files
                var newFiles = new List<BLL.Models.File>();

                if (vm.Files != null)
                {
                    newFiles.AddRange(
                        vm.Files
                            .Select(f => new BLL.Models.File(f.FileName, f.RelativePath, f.ContentType))
                            .ToList());
                }

                // new files
                filesFromForm = _filesManagement.ExtractFilesFromForm(
                    vm.FormFiles, vm.EditMaterialViewModel.MaterialId, guid, editRequest.EditRequestId);
                newFiles.AddRange(filesFromForm);

                editRequest.AddFiles(newFiles);
                _service.UpdateEditRequest(editRequest);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest("Materiał o podanym Id nie istnieje");
            }
            catch(Exception e)
            {
                _filesManagement.RemoveFilesFromFileSystem(filesFromForm);
                return BadRequest("Coś poszło nie tak podczas zapisywania plików. Spróbuj ponownie.");
            }

            return Json(vm.EditMaterialViewModel.MaterialId);
        }
    }
}