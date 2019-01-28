using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public sealed class LessonRepository : EFRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(DbSet<Lesson> dbSet) : base(dbSet)
        {
        }

        public new Lesson FindById(int lessonId)
        {
            return _dbSet
                .Include(l => l.Materials)
                .Include(l => l.Semester)
                .Include(l => l.Subject)
                .Include(l => l.Department)
                    .ThenInclude(d => d.University)
                //.Include(l => l.Department.University)
                .FirstOrDefault(l => l.LessonId == lessonId);
        }

        public bool IsSubjectWithSameNameOrAbbreviationExistInDepartment(Lesson lesson)
        {
            return _dbSet
                .Include(l => l.Subject)
                .Where(l => l.DepartmentId == lesson.DepartmentId)
                .Any(l => l.Subject.Name == lesson.Subject.Name || l.Subject.Abbreviation == lesson.Subject.Abbreviation);
        }

        public ICollection<Lesson> GetLessonsFromSemesterInDepartment(Semester semester, Department department)
        {
            return _dbSet
                .Include(l => l.Subject)
                .Where(l => l.SemesterId == semester.SemesterId && l.DepartmentId == department.DepartmentId)
                .ToList();
        }
    }
}
