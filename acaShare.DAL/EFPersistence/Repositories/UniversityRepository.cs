using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class UniversityRepository : EFRepository<University>, IUniversityRepository
    {
        public UniversityRepository(DbSet<University> dbSet) : base(dbSet)
        {
        }
    }
}
