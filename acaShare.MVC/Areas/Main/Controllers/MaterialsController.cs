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
        private readonly IFormFilesManagement _filesManagement;

        public MaterialsController(IMaterialsService service, IUniversityTreeTraversalService traversalService, IUserService userService, 
            IFormFilesManagement formFilesManagement)
        {
            _service = service;
            _traversalService = traversalService;
            _userService = userService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult Materials(int lessonId)
        {
            var lesson = _traversalService.GetLesson(lessonId);
            if (lesson == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "przedmiot o podanym Id nie istnieje." });
            }

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
            if (material == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "Materiał o podanym Id nie istnieje." });
            }

            var vm = PrepareMaterialViewModel(material);
            return View(vm);
        }

        public MaterialViewModel PrepareMaterialViewModel(BLL.Models.Material material)
        {
            ConfigureMaterialBreadcrumbs(material);

            var isFavorite = _userService.IsMaterialFavorite(material, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return new MaterialViewModel
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
        }


        public IActionResult Add(int lessonId)
        {
            var lesson = _traversalService.GetLesson(lessonId);
            if (lesson == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "przedmiot o podanym Id nie istnieje." });
            }

            ConfigureAddMaterialBreadcrumbs(lesson);

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
            if (lesson == null)
            {
                return BadRequest(new[] { "Przedmiot o podanym Id nie istnieje." });
            }

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
                return BadRequest(new[] { "Somethig went wrong while saving files to the file system. Try again." });
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
            if (materialToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return RedirectToAction("ActionForbidden", "Error", new { error = "nie masz uprawnień do tego działania" });
            }
            
            ConfigureEditMaterialBreadcrumbs(materialToEdit.Lesson, materialId);

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
            if (materialToEdit == null)
            {
                return BadRequest(new[] { "Materiał o podanym Id nie istnieje." });
            }

            if (identityUserId != materialToEdit.Creator.IdentityUserId)
            {
                return Forbid(new[] { "Nie masz uprawnień do tego działania" }); // TODO some authorization handler
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
                return BadRequest(new[] { "Coś poszło nie tak przy zapisywaniu plików do systemu plików. Spróbuj ponownie." });
            }

            var filesFromForm = _filesManagement.ExtractFilesFromForm(vm.FormFiles, vm.MaterialId, guid);
            materialToEdit.Update(vm.Name, vm.Description, filesFromForm, loggedUser);
            _service.UpdateMaterial(materialToEdit);

            return Json(vm.MaterialId);
        }

        public IActionResult Delete(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var materialToDelete = _service.GetMaterial(materialId);

            if (materialToDelete == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            if (identityUserId != materialToDelete.Creator.IdentityUserId)
            {
                return RedirectToAction("ActionForbidden", "Error", new { error = "nie masz uprawnień do tego działania" });
            }

            ConfigureDeleteMaterialBreadcrumbs(materialToDelete.Lesson, materialId);

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

            if (materialToDelete == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            if (identityUserId != materialToDelete.Creator.IdentityUserId)
            {
                return RedirectToAction("ActionForbidden", "Error", new { error = "nie masz uprawnień do tego działania" });
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
                    Title = subjectDepartment.Subject.Abbreviation + " - Materiały",
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
                    Title = "Materiał",
                    Params = parms
                }
            };
        }

        private void ConfigureAddMaterialBreadcrumbs(BLL.Models.Lesson lesson)
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
                    Title = "Dodawanie materiału",
                    Params = parms
                }
            };
        }

        private void ConfigureEditMaterialBreadcrumbs(BLL.Models.Lesson lesson, int materialId)
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
                { "lessonId", lesson.LessonId.ToString() },
                { "materialId", materialId.ToString() }
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
                    Action = "Edit",
                    Title = "Edycja materiału",
                    Params = parms
                }
            };
        }

        private void ConfigureEditSuggestionBreadcrumbs(BLL.Models.Lesson lesson, int materialId)
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
                { "lessonId", lesson.LessonId.ToString() },
                { "materialId", materialId.ToString() }
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
                    Action = "CreateEditSuggestion",
                    Title = "Tworzenie sugestii edycji",
                    Params = parms
                }
            };
        }

        private void ConfigureDeleteMaterialBreadcrumbs(BLL.Models.Lesson lesson, int materialId)
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
                { "lessonId", lesson.LessonId.ToString() },
                { "materialId", materialId.ToString() }
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
                    Action = "Delete",
                    Title = "Usuwanie materiału",
                    Params = parms
                }
            };
        }

        private void ConfigureDeleteSuggestionBreadcrumbs(BLL.Models.Lesson lesson, int materialId)
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
                { "lessonId", lesson.LessonId.ToString() },
                { "materialId", materialId.ToString() }
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
                    Action = "CreateDeleteSuggestion",
                    Title = "Tworzenie sugestii usunięcia",
                    Params = parms
                }
            };
        }
        #endregion


        public IActionResult ToggleFavorites(int materialId)
        {
            var loggedUser = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var material = _service.GetMaterial(materialId);

            if (material == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            _service.ToggleFavorite(material, loggedUser);

            return RedirectToAction("Material", new { @materialId = materialId });
        }

        [HttpPost]
        public IActionResult AddComment(AddCommentViewModel vm)
        {
            var material = _service.GetMaterial(vm.MaterialId);
            if (material == null)
            {
                ModelState.AddModelError("ERROR", "Taki materiał nie istnieje");
                RedirectToAction("Material", vm.MaterialId);
            }

            var materialViewModel = PrepareMaterialViewModel(material);

            if (!ModelState.IsValid)
            {
                return View("Material", materialViewModel);
            }

            var commentAuthor = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _service.AddComment(vm.NewComment, material, commentAuthor);

            return RedirectToAction("Material", new { @materialId = vm.MaterialId });
        }

        public IActionResult CreateDeleteSuggestion(int materialId, string materialName)
        {
            var materialToEdit = _service.GetMaterial(materialId);
            if (materialToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            ConfigureDeleteSuggestionBreadcrumbs(materialToEdit.Lesson, materialId);

            var reasons = _service.GetChangeReasons(BLL.Models.ChangeType.DELETE);

            var vm = new DeleteRequestViewModel
            {
                MaterialId = materialId,
                MaterialName = materialName,
                Reasons = reasons.ToList()
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
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje" });
            }

            return RedirectToAction("Material", new { materialId = vm.MaterialId });
        }


        public IActionResult CreateEditSuggestion(int materialId)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var materialToEdit = _service.GetMaterial(materialId);
            if (materialToEdit == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "materiał o podanym Id nie istnieje." });
            }

            if (identityUserId == materialToEdit.Creator.IdentityUserId)
            {
                return RedirectToAction(
                    "ActionForbidden",
                    "Error", 
                    new {
                        error = "nie masz uprawnień do tego działania. Jesteś autorem danego materiału - skorzystaj z opcji edycji"
                    });
            }

            ConfigureEditSuggestionBreadcrumbs(materialToEdit.Lesson, materialId);

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
            if (materialToEdit == null)
            {
                return BadRequest(new[] { "Materiał o podanym Id nie istnieje."});
            }

            if (identityUserId == materialToEdit.Creator.IdentityUserId)
            {
                return RedirectToAction(
                    "ActionForbidden",
                    "Error",
                    new
                    {
                        error = "nie masz uprawnień do tego działania. Jesteś autorem danego materiału - skorzystaj z opcji edycji"
                    });
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
                return BadRequest(new[] { "Materiał o podanym Id nie istnieje" });
            }
            catch(Exception e)
            {
                _filesManagement.RemoveFilesFromFileSystem(filesFromForm);
                return BadRequest(new[] { "Coś poszło nie tak podczas zapisywania plików. Spróbuj ponownie." });
            }

            return Json(vm.EditMaterialViewModel.MaterialId);
        }
    }
}