﻿using acaShare.BLL.Models;
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

        public void AddUpdateRequest(EditRequest editRequest)
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
                .Include(m => m.Lesson.SubjectDepartment)
                .Include(m => m.Lesson.SubjectDepartment.Subject)
                .Include(m => m.Lesson.SubjectDepartment.Department)
                .Include(m => m.Lesson.SubjectDepartment.Department.University)
                .First(m => m.MaterialId == materialId);
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
                .First(m => m.MaterialId == materialId);
        }

        private IQueryable<Material> GetMaterialToApproveWithIncludes()
        {
            return _dbSet
                .Include(m => m.Creator)
                .Include(m => m.Lesson)
                .Include(m => m.Lesson.Semester)
                .Include(m => m.Lesson.SubjectDepartment)
                .Include(m => m.Lesson.SubjectDepartment.Subject)
                .Include(m => m.Lesson.SubjectDepartment.Department)
                .Include(m => m.Lesson.SubjectDepartment.Department.University);
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

        public void ApproveDeleteRequest(DeleteRequest deleteRequest)
        {
            _deleteRequests.Update(deleteRequest);
        }
    }
}
