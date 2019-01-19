using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class MaterialRepository : EFRepository<Material>, IMaterialRepository
    {
        private readonly DbSet<File> _files;
        private readonly DbSet<ChangeReason> _changeReasons;
        private readonly DbSet<DeleteRequest> _deleteRequests;
        private readonly DbSet<EditRequest> _editRequests;

        public MaterialRepository(DbSet<Material> materials, DbSet<File> files, DbSet<ChangeReason> changeReasons, 
            DbSet<DeleteRequest> deleteRequests, DbSet<EditRequest> editRequests) : base(materials)
        {
            _files = files;
            _changeReasons = changeReasons;
            _deleteRequests = deleteRequests;
            _editRequests = editRequests;
        }

        public void AddDeleteRequest(DeleteRequest deleteRequest)
        {
            _deleteRequests.Add(deleteRequest);
        }

        public void AddEditRequest(EditRequest editRequest)
        {
            _editRequests.Add(editRequest);
        }

        public new Material FindById(int materialId)
        {
            return _dbSet
                .Include(m => m.State)
                .Include(m => m.Updater)
                .Include(m => m.Creator)
                .Include(m => m.Approver)
                .Include(m => m.Lesson)
                .Include(m => m.Lesson.Semester)
                .Include(m => m.Lesson.Subject)
                .Include(m => m.Lesson.Department)
                    .ThenInclude(m => m.University)
                .FirstOrDefault(m => m.MaterialId == materialId);
        }

        public new void Delete(Material material)
        {
            var deleteRequests = material.DeleteRequests.Where(dr => dr.RequestState == RequestState.PENDING);
            _deleteRequests.RemoveRange(deleteRequests);

            // Edit requests connected with this material are cascade deleted, but the material's files and it's edit requests' files 
            // are not (since onDelete action is RESTRICT on material's files and SET NULL on edit request's files due to cyclic cascade deletes) 
            // so we remove them manually before deleting a material.
            var materialFiles = material.Files;
            _files.RemoveRange(materialFiles);

            var editRequestFiles = material.EditRequests.SelectMany(er => er.Files);
            _files.RemoveRange(editRequestFiles);

            base.Delete(material);
        }

        public ICollection<Material> GetMaterialsToApprove()
        {
            return GetMaterialToApproveWithIncludes()
                .Where(m => m.StateId == 3)
                .OrderByDescending(m => m.UploadDate)
                .ToList();
        }

        public Material GetMaterialToApprove(int materialId)
        {
            return GetMaterialToApproveWithIncludes()
                .FirstOrDefault(m => m.MaterialId == materialId);
        }

        private IQueryable<Material> GetMaterialToApproveWithIncludes()
        {
            return _dbSet
                .Include(m => m.Creator)
                .Include(m => m.Lesson)
                .Include(m => m.Lesson.Semester)
                .Include(m => m.Lesson.Subject)
                .Include(m => m.Lesson.Department)
                .Include(m => m.Lesson.Department.University);
        }

        public ICollection<ChangeReason> GetChangeReasons(ChangeType changeType)
        {
            return _changeReasons.Where(cr => cr.ChangeType == changeType).ToList();
        }

        public ICollection<DeleteRequest> GetDeleteRequests(RequestState requestState)
        {
            return _deleteRequests
                .Include(r => r.Deleter)
                .Include(r => r.DeleteReason)
                .Include(r => r.MaterialToDelete)
                .Where(r => r.RequestState == requestState)
                .ToList();
        }

        public DeleteRequest GetDeleteRequest(int deleteRequestId)
        {
            return _deleteRequests
                .Include(r => r.Deleter)
                .Include(r => r.DeleteReason)
                .Include(r => r.MaterialToDelete)
                .Include(r => r.MaterialToDelete.Creator)
                .FirstOrDefault(r=> r.DeleteRequestId == deleteRequestId);
        }

        public void UpdateEditRequest(EditRequest editRequest)
        {
            _editRequests.Update(editRequest);
        }

        public void UpdateDeleteRequest(DeleteRequest deleteRequest)
        {
            _deleteRequests.Update(deleteRequest);
        }

        public ICollection<EditRequest> GetEditRequests()
        {
            return _editRequests
                .Include(r => r.Updater)
                .Include(r => r.MaterialToUpdate)
                .Include(r => r.Files)
                .ToList();
        }

        public EditRequest GetEditRequest(int editRequestId)
        {
            return _editRequests
                .Include(r => r.Updater)
                .Include(r => r.MaterialToUpdate)
                .Include(r => r.MaterialToUpdate.Creator)
                .Include(r => r.Files)
                .FirstOrDefault(r => r.EditRequestId == editRequestId);
        }

        public void DeleteEditRequest(EditRequest editRequest)
        {
            _editRequests.Remove(editRequest);
        }

        public void DeleteEditRequests(ICollection<EditRequest> editRequests)
        {
            _editRequests.RemoveRange(editRequests);
        }

        public void RemoveFiles(IEnumerable<File> filesToRemove)
        {
            _files.RemoveRange(filesToRemove);
        }
    }
}
