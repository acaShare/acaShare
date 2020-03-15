using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Common;
using acaShare.WebAPI.Models;
using acaShare.WebAPI.Models.MaterialChangeRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace acaShare.WebAPI.Controllers.MaterialChangeRequests
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeleteSuggestionsController : ControllerBase
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<DeleteRequestViewModel> Get()
        {
            return _materialsService.GetPendingDeleteSuggestions()
                .Select(ds =>
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
        }

        [HttpGet("{deleteRequestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DeleteRequestApprovalDecision> Get(int deleteRequestId)
        {
            var deleteRequest = _materialsService.GetDeleteRequest(deleteRequestId);
            if (deleteRequest == null)
            {
                return NotFound();
            }

            return new DeleteRequestApprovalDecision
            {
                MaterialViewModel = new MaterialViewModel
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
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ChangeSuggestionState(DeleteRequestApprovalDecisionDto dto)
        {
            var loggedModerator = _userService.FindByIdentityUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                if (dto.ShouldApprove)
                {
                    var deleteRequest = _materialsService.GetDeleteRequest(dto.DeleteRequestId);
                    if (deleteRequest == null)
                    {
                        return NotFound();
                    }
                
                    _filesManagement.DeleteWholeMaterialFolder(deleteRequest.MaterialToDeleteId.Value);
                    _materialsService.ApproveDeleteRequest(deleteRequest, loggedModerator);
                }
                else
                {
                    _materialsService.DeclineDeleteRequest(dto.DeleteRequestId, loggedModerator, dto.DeclineReason);
                }
            }
            catch (ArgumentException)
            {
                return BadRequest("Sugestia usunięcia o podanym Id nie istnieje");
            }

            return NoContent();
        }
    }
}