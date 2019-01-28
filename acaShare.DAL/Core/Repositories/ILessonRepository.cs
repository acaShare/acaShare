using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        bool IsSubjectWithSameNameOrAbbreviationExistInDepartment(Lesson lesson);
        ICollection<Lesson> GetLessonsFromSemesterInDepartment(Semester semester, Department department);
    }
}
