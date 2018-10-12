using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class LessonRepository : EFRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(DbSet<Lesson> dbSet) : base(dbSet)
        {
        }
    }
}
