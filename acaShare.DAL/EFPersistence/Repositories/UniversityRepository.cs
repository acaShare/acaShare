using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class UniversityRepository : EFRepository<University>, IUniversityRepository
    {
        public UniversityRepository(DbSet<University> dbSet) : base(dbSet)
        {
        }

        public IEnumerable<Department> GetDepartmentsFromUniversity(int universityId)
        {
            IEnumerable<Department> departments = _dbSet.FirstOrDefault(d => d.UniversityId == universityId)?.Departments;
            return departments;
        }

        public bool DoesUniversityAlreadyExist(University university)
        {
            return _dbSet.Any(u => (u.Name == university.Name || u.Abbreviation == university.Abbreviation) && u.UniversityId != university.UniversityId);
        }
    }
}
