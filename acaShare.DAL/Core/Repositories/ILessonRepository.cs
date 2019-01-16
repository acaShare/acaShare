using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        bool DoesLessonAlreadyExist(int subjectDepartmentId, int semesterId);
        bool IsAbbreviationAlreadyTaken(int deptId, int semesterId, string abbreviation);
    }
}
