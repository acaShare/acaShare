using acaShare.BLL.Models;
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
        void UpdateMaterial(Material material, ICollection<File> filesToRemove);
        void DeleteMaterial(Material materialToDelete);
    }

    public enum MaterialStateEnum
    {
        PENDING, APPROVED, REJECTED
    }
}
