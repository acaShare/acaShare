using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using acaShare.DAL.Core.Repositories.UserRelated;
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

        public IUniversityRepository Universities { get; }
        public IDepartmentRepository Departments { get; }
        public ISemesterRepository Semesters { get; }
        public ISubjectRepository Subjects { get; }
        public ILessonRepository Lessons { get; }
        public IUserRepository Users { get; set; }
        public IMaterialRepository Materials { get; set; }

        public UnitOfWork(AcaShareDbContext dbContext)
        {
            _db = dbContext;
            Universities = new UniversityRepository(_db.University);
            Departments = new DepartmentRepository(_db.Department);
            Semesters = new SemesterRepository(_db.Semester);
            Subjects = new SubjectRepository(_db.Subject);
            Lessons = new LessonRepository(_db.Lesson);
            Users = new UserRepository(_db.User);
            Materials = new MaterialRepository(_db.Material);
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
