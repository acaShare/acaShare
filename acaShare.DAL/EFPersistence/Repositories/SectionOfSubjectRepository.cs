using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class SectionOfSubjectRepository : EFRepository<SectionOfSubject>, ISectionOfSubjectRepository
    {
        public SectionOfSubjectRepository(DbSet<SectionOfSubject> dbSet) : base(dbSet)
        {
        }
    }
}
