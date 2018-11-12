using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class DepartmentRepository : EFRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbSet<Department> dbSet) : base(dbSet)
        {
        }

        public IEnumerable<SubjectDepartment> FindSubjectDepartmentAssociations(int departmentId)
        {
            return _dbSet.Find(departmentId).SubjectDepartment;
        }
    }
}
