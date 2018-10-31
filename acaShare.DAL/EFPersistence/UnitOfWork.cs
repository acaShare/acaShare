using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using acaShare.DAL.EFPersistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AcaShareDbContext _db;

        public ILessonRepository Lessons { get; }
        public IUniversityRepository Universities { get; }
        public IDepartmentRepository Departments { get; }
        public ILecturerRepository Lecturers { get; }
        public ISectionOfSubjectRepository SectionsOfSubject { get; }
        public ISubjectRepository Subjects { get; }

        public UnitOfWork(AcaShareDbContext dbContext)
        {
            _db = dbContext;
            Lessons = new LessonRepository(_db.Lesson);
            Universities = new UniversityRepository(_db.University);
            Departments = new DepartmentRepository(_db.Department);
            Lecturers = new LecturerRepository(_db.Lecturer);
            SectionsOfSubject = new SectionOfSubjectRepository(_db.SectionOfSubject);
            Subjects = new SubjectRepository(_db.Subject);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
