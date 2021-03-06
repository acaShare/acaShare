﻿using acaShare.BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface IMaterialsService
    {
        ICollection<Material> GetAllMaterials();
        void AddMaterial(Material material);
        void CreateDeleteRequest(User deleter, int materialToDeleteId, int reasonId, string additionalComment = null);
        void CreateUpdateRequest(Material material);
        Material GetMaterial(int materialId);
        MaterialState GetState(MaterialStateEnum materialStateEnum);
        void AddComment(string newComment, Material material, User commentAuthor);
        ICollection<DeleteRequest> GetPendingDeleteSuggestions();
        void ToggleFavorite(Material material, User loggedUser);
        void UpdateMaterial(Material material);
        ICollection<EditRequest> GetPendingEditSuggestions();
        void DeleteMaterial(Material materialToDelete);
        ICollection<Material> GetMaterialsToApprove();
        void RejectMaterial(Material material);
        Material GetMaterialToApprove(int materialId);
        void ApproveMaterial(Material material, User approver);
        ICollection<ChangeReason> GetChangeReasons(ChangeType changeType);
        DeleteRequest GetDeleteRequest(int deleteRequestId);
        void ApproveDeleteRequest(DeleteRequest deleteRequest, User loggedModerator);
        EditRequest GetEditRequest(int editRequestId);
        void DeclineDeleteRequest(int deleteRequestId, User loggedModerator, string declineReason);
        EditRequest CreateEditRequest(User updater, Material materialToUpdate, string editSummary, string newName, string newDescription);
        void UpdateEditRequest(EditRequest editRequest);
        void DeclineEditRequest(int editRequestId, string declineReason);
        void ApproveEditRequest(EditRequest editRequest);
    }

    public enum MaterialStateEnum
    {
        PENDING, APPROVED, REJECTED
    }
}
