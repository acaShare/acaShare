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

        public MaterialRepository(DbSet<Material> materials, DbSet<File> files) : base(materials)
        {
            _files = files;
        }

        public void CreateDeleteRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public void CreateUpdateRequest(Material material)
        {
            throw new NotImplementedException();
        }

        public new Material FindById(int materialId)
        {
            return _dbSet
                .Include(m => m.Lesson)
                .Include(m => m.State)
                .Include(m => m.Updater)
                .Include(m => m.Files)
                .Include(m => m.Favorites)
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
    }
}
