using acaShare.DAL.Core;
using acaShare.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILessonRepository Lessons { get; }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
