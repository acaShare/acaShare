using acaShare.BLL.Models;
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
        void DeleteMaterial(Material materialToDelete);
        ICollection<Material> GetMaterialsToApprove();
        void RejectMaterial(int materialId);
        Material GetMaterialToApprove(int materialId);
        void ApproveMaterial(int materialId, User approver);
        ICollection<ChangeReason> GetChangeReasons(ChangeType changeType);
        DeleteRequest GetDeleteRequestToApprove(int deleteRequestId);
        void ApproveRequest(int deleteRequestId, User loggedModerator);
    }

    public enum MaterialStateEnum
    {
        PENDING, APPROVED, REJECTED
    }
}
