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
    public sealed class SubjectRepository : EFRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(DbSet<Subject> dbSet) : base(dbSet)
        {
        }

        public Subject GetByNaming(string name, string abbreviation)
        {
            return _dbSet.FirstOrDefault(s => s.Name == name && s.Abbreviation == abbreviation);
        }
    }
}
