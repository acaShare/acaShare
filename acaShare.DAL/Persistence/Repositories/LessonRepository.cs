using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Persistence.Repositories
{
    public class LessonRepository : DapperRepository<Lesson>, ILessonRepository
    {
    }
}
