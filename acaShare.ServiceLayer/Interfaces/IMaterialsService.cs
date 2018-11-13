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
    }
}
