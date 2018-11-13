using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IUnitOfWork _uow;

        public MaterialsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Material GetMaterial(int materialId)
        {
            return _uow.Materials.FindById(materialId);
        }

        public ICollection<Material> GetAllMaterials()
        {
            return _uow.Materials.GetAll();
        }

        public void AddMaterial(Material material)
        {
            _uow.Materials.Add(material);
            _uow.SaveChanges();
        }

        public void CreateDeleteRequest(int deleterId, int materialToDeleteId)
        {
            var deleter = _uow.Users.FindById(deleterId);
            var materialToDelete = _uow.Materials.FindById(materialToDeleteId);

            DeleteRequest deleteRequest = new DeleteRequest(deleter, materialToDelete);
            _uow.SaveChanges();
        }

        public void CreateUpdateRequest(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
