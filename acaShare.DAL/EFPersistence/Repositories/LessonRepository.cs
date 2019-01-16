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
                .Include(l => l.SubjectDepartment)
                .Include(l => l.SubjectDepartment.Subject)
                .Include(l => l.SubjectDepartment.Department)
                .Include(l => l.SubjectDepartment.Department.University)
                .FirstOrDefault(l => l.LessonId == lessonId);
        }

        public bool DoesLessonAlreadyExist(int subjectDepartmentId, int semesterId)
        {
            return _dbSet.Any(l => l.SubjectDepartmentId == subjectDepartmentId && l.SemesterId == semesterId);
        }

        public bool IsAbbreviationAlreadyTaken(int deptId, int semesterId, string abbreviation)
        {
            foreach (var lesson in _dbSet)
            {
                if (lesson.SubjectDepartment.DepartmentId == deptId && lesson.SemesterId == semesterId)
                {
                    if (lesson.SubjectDepartment.Subject.Abbreviation == abbreviation)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
