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

        public new Department FindById(int departmentId)
        {
            return _dbSet
                .Include(d => d.University)
                .FirstOrDefault(d => d.DepartmentId == departmentId);
        }

        public bool DoesDepartmentAlreadyExistInUniversity(Department department)
        {
            // department with supplied name or abbreviation in supplied university but not it itself
            return _dbSet.Any(d => 
                (d.Name == department.Name || d.Abbreviation == department.Abbreviation) && 
                d.UniversityId == department.UniversityId && 
                d.DepartmentId != department.DepartmentId);
        }
    }
}
