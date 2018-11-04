using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class SemesterRepository : EFRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(DbSet<Semester> dbSet) : base(dbSet)
        {
        }
    }
}
