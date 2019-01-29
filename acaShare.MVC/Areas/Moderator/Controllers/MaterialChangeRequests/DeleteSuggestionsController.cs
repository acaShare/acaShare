using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models;
using acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests;
using acaShare.MVC.Common;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.MaterialChangeRequests
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [Area("Moderator")]
    public class DeleteSuggestionsController : Controller
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;
        private readonly IFormFilesManagement _filesManagement;

        public DeleteSuggestionsController(IMaterialsService materialsService, IUserService userService, IFormFilesManagement formFilesManagement)
        {
            _materialsService = materialsService;
            _userService = userService;
            _filesManagement = formFilesManagement;
        }

        public IActionResult DeleteSuggestions()
        {
            ConfigureBreadcrumbs();

            var vms = _materialsService.GetPendingDeleteSuggestions().Select(ds =>
                new DeleteRequestViewModel
                {
                    DeleteRequestId = ds.DeleteRequestId,
                    MaterialName = ds.MaterialToDelete.Name,
                    ReasonId = ds.DeleteReasonId,
                    Reason = ds.DeleteReason.Reason,
                    AdditionalComment = ds.AdditionalComment,
                    DeleterName = ds.Deleter.Username,
                    RequestDate = ds.RequestDate
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
                    Controller = "DeleteSuggestions",
                    Action = "DeleteSuggestions",
                    Title = "Sugestie usunięcia"
                }
            };
        }

        public IActionResult DeleteRequestApprovalDecision(int deleteRequestId)
        {
            var deleteRequest = _materialsService.GetDeleteRequest(deleteRequestId);
            if (deleteRequest == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia usunięcia o podanym Id nie istnieje." });
            }

            ConfigureSuggestionBreadcrumbs(deleteRequestId);

            var vm = new ChangeRequestApprovalDecision
            {
                MaterialViewModel = new MaterialToApproveViewModel
                {
                    MaterialId = deleteRequest.MaterialToDeleteId.Value,
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

        private void ConfigureSuggestionBreadcrumbs(int deleteRequestId)
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "DeleteSuggestions",
                    Action = "DeleteRequestApprovalDecision",
                    Title = "Podgląd materiału",
                    Params = new Dictionary<string, string>() { { "deleteRequestId", deleteRequestId.ToString() } }
                }
            };
        }

        public IActionResult ApproveDeleteRequest(int deleteRequestId)
        {
            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var deleteRequest = _materialsService.GetDeleteRequest(deleteRequestId);
                if (deleteRequest == null)
                {
                    return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia usunięcia o podanym Id nie istnieje." });
                }

                _filesManagement.DeleteWholeMaterialFolder(deleteRequest.MaterialToDeleteId.Value);
                _materialsService.ApproveDeleteRequest(deleteRequest, loggedModerator);
            }
            catch(ArgumentException)
            {
                return BadRequest("Sugestia usunięcia o podanym Id nie istnieje");
            }

            return RedirectToAction("DeleteSuggestions");
        }

        public IActionResult DeclineDeleteRequest(int deleteRequestId)
        {
            var deleteRequest = _materialsService.GetDeleteRequest(deleteRequestId);
            if (deleteRequest == null)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia usunięcia o podanym Id nie istnieje." });
            }

            var vm = new DeleteRequestViewModel
            {
                DeleteRequestId = deleteRequestId,
                MaterialName = deleteRequest.MaterialToDelete.Name,
                ReasonId = deleteRequest.DeleteReasonId,
                Reason = deleteRequest.DeleteReason.Reason
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult DeclineDeleteRequest(DeleteRequestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                _materialsService.DeclineDeleteRequest(vm.DeleteRequestId, loggedModerator, vm.DeclineReason);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("ResourceNotFound", "Error", new { error = "sugestia usunięcia o podanym Id nie istnieje." });
            }

            return RedirectToAction("DeleteSuggestions");
        }
    }
}