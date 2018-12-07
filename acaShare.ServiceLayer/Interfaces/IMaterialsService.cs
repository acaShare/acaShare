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
        void CreateDeleteRequest(int deleterId, int materialToDeleteId);
        void CreateUpdateRequest(Material material);
        Material GetMaterial(int materialId);
        MaterialState GetState(MaterialStateEnum materialStateEnum);
        void AddComment(string newComment, Material material, User commentAuthor);
        void ToggleFavorite(Material material, User loggedUser);
        void UpdateMaterial(Material material);
        void DeleteMaterial(Material materialToDelete);
        ICollection<Material> GetMaterialsToApprove();
        void RejectMaterial(int materialId);
        Material GetMaterialToApprove(int materialId);
        void ApproveMaterial(int materialId, User approver);
    }

    public enum MaterialStateEnum
    {
        PENDING, APPROVED, REJECTED
    }
}
