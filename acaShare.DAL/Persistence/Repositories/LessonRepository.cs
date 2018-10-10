using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace acaShare.DAL.Persistence.Repositories
{
    public sealed class LessonRepository : DapperRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
