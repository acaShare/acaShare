using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests;
using acaShare.MVC.Common;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Controllers.MaterialChangeRequests
{
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
            var vms = _materialsService.GetPendingEditSuggestions().Select(es =>
                new EditRequestViewModel
                {
                    EditRequestId = es.EditRequestId,
                    MaterialName = es.MaterialToUpdate.Name,
                    Summary = es.Summary
                }
            ).ToList();

            return View(vms);
        }

        public IActionResult EditRequestApprovalDecision(int editRequestId)
        {
            BLL.Models.EditRequest editRequest = _materialsService.GetEditRequest(editRequestId);

            if (editRequest == null)
            {
                return BadRequest("Sugestia edycji o podanym Id nie istnieje");
            }

            var vm = new ChangeRequestApprovalDecision
            {
                MaterialViewModel = new MaterialToApproveViewModel
                {
                    MaterialId = editRequest.MaterialToUpdateId.Value,
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

        public IActionResult ApproveEditRequest(int editRequestId)
        {
            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var editRequest = _materialsService.GetEditRequest(editRequestId);

            try
            {
                int materialToUpdateId = editRequest.MaterialToUpdateId.Value;
                _materialsService.ApproveEditRequest(editRequest);
                _filesManagement.ReplaceMaterialFilesWithEditRequestFiles(materialToUpdateId, editRequest.EditRequestId, editRequest.Files);
            }
            catch (ArgumentException e)
            {
                return BadRequest("Sugestia edycji o podanym Id nie istnieje");
            }
            catch(Exception e)
            {
                _filesManagement.RemoveFilesFromFileSystem(editRequest.Files);
                return BadRequest("Coś poszło nie tak podczas zapisywania plików. Spróbuj ponownie.");
            }
            
            return RedirectToAction("EditSuggestions");
        }

        public IActionResult DeclineEditRequest(int editRequestId)
        {
            BLL.Models.EditRequest editRequest = _materialsService.GetEditRequest(editRequestId);

            if (editRequest == null)
            {
                return BadRequest("Sugestia edycji o podanym Id nie istnieje");
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
            catch (ArgumentException e)
            {
                return BadRequest("Sugestia edycji o podanym Id nie istnieje");
            }

            return RedirectToAction("EditSuggestions");
        }
    }
}
