using acaShare.DAL.Core.Repositories;
using acaShare.DAL.Core.Repositories.UniversityRelated;
using acaShare.DAL.Core.Repositories.UserRelated;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILessonRepository Lessons { get; }
        IDepartmentRepository Departments { get; }
        ILecturerRepository Lecturers { get; }
        ISectionOfSubjectRepository SectionsOfSubject { get; }
        ISubjectRepository Subjects { get; }
        IUniversityRepository Universities { get; }
        IUserRepository Users { get; set; }
        void SaveChanges();
    }
}
