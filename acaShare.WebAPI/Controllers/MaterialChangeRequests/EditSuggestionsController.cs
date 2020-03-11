using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
using acaShare.WebAPI.Models;
using acaShare.WebAPI.Models.MaterialChangeRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace acaShare.WebAPI.Controllers.MaterialChangeRequests
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [Area("Moderator")]
    public class EditSuggestionsController : Controller
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;
        private readonly IFormFilesManagement _filesManagement;

        public EditSuggestionsController(IMaterialsService materialsService, IUserService userService, IFormFilesManagement formFilesManagement)
        {
            _materialsService = materialsService;
            _userService = userService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult EditSuggestions()
        {
            ConfigureBreadcrumbs();

            var vms = _materialsService.GetPendingEditSuggestions().Select(es =>
                new EditRequestViewModel
                {
                    EditRequestId = es.EditRequestId,
                    MaterialName = es.MaterialToUpdate.Name,
                    Summary = es.Summary,
                    RequestDate = es.RequestDate
                }
            ).ToList();

            return View(vms);
        }

        private void ConfigureBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "EditSuggestions",
                    Action = "EditSuggestions",
                    Title = "Sugestie edycji"
                }
            };
        }

        public IActionResult EditRequestApprovalDecision(int editRequestId)
        {
            var editRequest = _materialsService.GetEditRequest(editRequestId);
            if (editRequest == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia edycji o podanym Id nie istnieje." });
            }

            ConfigureSuggestionBreadcrumbs(editRequestId);

            var vm = new EditRequestApprovalDecision
            {
                MaterialViewModel = new MaterialViewModel
                {
                    MaterialId = editRequest.MaterialToUpdateId,
                    CreatorUsername = editRequest.MaterialToUpdate.Creator.Username,
                    Name = editRequest.MaterialToUpdate.Name,
                    Description = editRequest.MaterialToUpdate.Description,
                    UploadDate = editRequest.MaterialToUpdate.UploadDate
                },
                EditRequestViewModel = new EditRequestViewModel
                {
                    EditRequestId = editRequest.EditRequestId,
                    MaterialName = editRequest.NewName,
                    NewDescription = editRequest.NewDescription,
                    Summary = editRequest.Summary,
                    UpdaterName = editRequest.Updater.Username,
                    RequestDate = editRequest.RequestDate,
                    Files = editRequest.Files.Select(f =>
                       new FileViewModel
                       {
                           FileId = f.FileId,
                           FileName = f.FileName,
                           RelativePath = f.RelativePath,
                           ContentType = f.ContentType
                       }
                    ).ToList()
                }
            };

            return View(vm);
        }

        private void ConfigureSuggestionBreadcrumbs(int editRequestId)
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "EditSuggestions",
                    Action = "EditRequestApprovalDecision",
                    Title = "Podgląd zmian",
                    Params = new Dictionary<string, string>() { { "editRequestId", editRequestId.ToString() } }
                }
            };
        }

        public IActionResult ApproveEditRequest(int editRequestId, bool isRedirectToMaterial)
        {
            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var editRequest = _materialsService.GetEditRequest(editRequestId);
            if (editRequest == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia edycji o podanym Id nie istnieje." });
            }

            try
            {
                int materialToUpdateId = editRequest.MaterialToUpdateId;
                _materialsService.ApproveEditRequest(editRequest);
                _filesManagement.ReplaceMaterialFilesWithEditRequestFiles(materialToUpdateId, editRequest.EditRequestId, editRequest.Files);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia edycji o podanym Id nie istnieje." });
            }
            catch (Exception)
            {
                _filesManagement.RemoveFilesFromFileSystem(editRequest.Files);
                return BadRequest("Coś poszło nie tak podczas zapisywania plików. Spróbuj ponownie.");
            }

            if (isRedirectToMaterial)
            {
                return RedirectToAction("Material", "Materials", new { area = "Main", materialId = editRequest.MaterialToUpdateId });
            }

            return RedirectToAction("EditSuggestions");
        }

        public IActionResult DeclineEditRequest(int editRequestId)
        {
            var editRequest = _materialsService.GetEditRequest(editRequestId);
            if (editRequest == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia edycji o podanym Id nie istnieje." });
            }

            var vm = new EditRequestViewModel
            {
                EditRequestId = editRequestId,
                MaterialName = editRequest.MaterialToUpdate.Name,
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeclineEditRequest(EditRequestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                _materialsService.DeclineEditRequest(vm.EditRequestId, vm.DeclineReason);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia edycji o podanym Id nie istnieje." });
            }

            return RedirectToAction("EditSuggestions");
        }
    }
}
