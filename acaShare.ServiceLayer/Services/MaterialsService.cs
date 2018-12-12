using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateDeleteRequest(User deleter, int materialToDeleteId, int reasonId, string additionalComment = null)
        {
            var materialToDelete = _uow.Materials.FindById(materialToDeleteId);

            DeleteRequest deleteRequest = new DeleteRequest(deleter, materialToDelete, reasonId, additionalComment);
            _uow.Materials.AddDeleteRequest(deleteRequest);
            _uow.SaveChanges();
        }

        public void CreateUpdateRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public MaterialState GetState(MaterialStateEnum materialStateEnum)
        {
            return _uow.MaterialStates.GetAll().First(ms => ms.Name == materialStateEnum.ToString());
        }

        public void AddComment(string newComment, Material material, User commentAuthor)
        {
            Comment comment = new Comment(newComment, commentAuthor);
            material.AddComment(comment);
            UpdateMaterial(material);
        }

        public void ToggleFavorite(Material material, User loggedUser)
        {
            loggedUser.ToggleFavorite(material);
            _uow.Users.Update(loggedUser);
            _uow.SaveChanges();
        }
        
        public void UpdateMaterial(Material material)
        {
            _uow.Materials.Update(material);
            _uow.SaveChanges();
        }

        public void DeleteMaterial(Material materialToDelete)
        {
            _uow.Materials.Delete(materialToDelete);
            _uow.SaveChanges();
        }

        public ICollection<Material> GetMaterialsToApprove()
        {
            return _uow.Materials.GetMaterialsToApprove();
        }

        public void ApproveMaterial(int materialId, User approver)
        {
            var material = GetMaterial(materialId);
            material.Approve(approver);
            UpdateMaterial(material);
        }

        public void RejectMaterial(int materialId)
        {
            var material = GetMaterial(materialId);
            DeleteMaterial(material);
            // If something in the future related to materials rejection was made, this code will be used
            //var material = GetMaterial(materialId);
            //material.Reject();
            //UpdateMaterial(material);
        }

        public Material GetMaterialToApprove(int materialId)
        {
            return _uow.Materials.GetMaterialToApprove(materialId);
        }

        public ICollection<ChangeReason> GetChangeReasons(ChangeType changeType)
        {
            return _uow.Materials.GetChangeReasons(changeType);
        }

        public ICollection<DeleteRequest> GetPendingDeleteSuggestions()
        {
            return _uow.Materials.GetDeleteRequests(RequestState.PENDING);
        }

        public DeleteRequest GetDeleteRequestToApprove(int deleteRequestId)
        {
            return _uow.Materials.GetDeleteRequest(deleteRequestId);
        }

        public void ApproveRequest(int deleteRequestId, User loggedModerator)
        {
            var deleteRequest = _uow.Materials.GetDeleteRequest(deleteRequestId);
            deleteRequest.ApproveRequest(loggedModerator);
            _uow.Materials.ApproveDeleteRequest(deleteRequest);
            _uow.SaveChanges();
        }
    }
}
