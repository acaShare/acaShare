using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.MaterialChangeRequests
{
    [Area("Moderator")]
    public class DeleteSuggestionsController : Controller
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;

        public DeleteSuggestionsController(IMaterialsService materialsService, IUserService userService)
        {
            _materialsService = materialsService;
            _userService = userService;
        }

        public IActionResult DeleteSuggestions()
        {
            var vms = _materialsService.GetPendingDeleteSuggestions().Select(ds =>
                new DeleteRequestViewModel
                {
                    DeleteRequestId = ds.DeleteRequestId,
                    MaterialName = ds.MaterialToDelete.Name,
                    ReasonId = ds.DeleteReasonId,
                    Reason = ds.DeleteReason.Reason,
                    AdditionalComment = ds.AdditionalComment,
                    DeleterName = ds.Deleter.Username
                }
            ).ToList();

            return View(vms);
        }


        public IActionResult DeleteRequestApprovalDecision(int deleteRequestId)
        {
            BLL.Models.DeleteRequest deleteRequest = _materialsService.GetDeleteRequestToApprove(deleteRequestId);

            if (deleteRequest == null)
            {
                return BadRequest("Sugestia usunięcia o podanym Id nie istnieje");
            }

            var vm = new ChangeRequestApprovalDecision
            {
                MaterialViewModel = new MaterialToApproveViewModel
                {
                    MaterialId = deleteRequest.MaterialToDeleteId,
                    CreatorUsername = deleteRequest.MaterialToDelete.Creator.Username,
                    Name = deleteRequest.MaterialToDelete.Name,
                    Description = deleteRequest.MaterialToDelete.Description,
                    UploadDate = deleteRequest.MaterialToDelete.UploadDate,
                    Files = deleteRequest.MaterialToDelete.Files.Select(f => 
                        new FileViewModel
                        {
                            FileId = f.FileId,
                            FileName = f.FileName,
                            RelativePath = f.RelativePath,
                            ContentType = f.ContentType
                        }
                    ).ToList()
                },
                DeleteRequestViewModel = new DeleteRequestViewModel
                {
                    DeleteRequestId = deleteRequest.DeleteRequestId,
                    ReasonId = deleteRequest.DeleteReasonId,
                    Reason = deleteRequest.DeleteReason.Reason,
                    AdditionalComment = deleteRequest.AdditionalComment,
                    DeleterName = deleteRequest.Deleter.Username
                }
            };

            return View(vm);
        }
        
        public IActionResult ApproveDeleteRequest(int deleteRequestId)
        {
            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _materialsService.ApproveRequest(deleteRequestId, loggedModerator);

            return RedirectToAction("DeleteSuggestions");
        }
        
        public IActionResult DeclineDeleteRequest()
        {
            return RedirectToAction("DeleteSuggestions");
        }
    }
}