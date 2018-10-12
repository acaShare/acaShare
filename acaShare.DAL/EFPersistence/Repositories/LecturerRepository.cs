using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class LecturerRepository : EFRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(DbSet<Lecturer> dbSet) : base(dbSet)
        {
        }
    }
}
