using acaShare.BLL.Models;
using acaShare.DAL.Configuration;
using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using acaShare.DAL.EFPersistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.EFPersistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly acaShareGenerateContext _db;

        public ILessonRepository Lessons { get; }

        public UnitOfWork(acaShareGenerateContext dbContext)
        {
            _db = dbContext;
            Lessons = new LessonRepository(_db.Lesson);
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
