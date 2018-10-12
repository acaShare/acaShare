using acaShare.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILessonRepository Lessons { get; }
        IUniversityRepository Universities { get; }
        void SaveChanges();
    }
}
