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
        IUniversityRepository Universities { get; }
        IDepartmentRepository Departments { get; }
        ISemesterRepository Semesters { get; }
        ISubjectRepository Subjects { get; }
        ILessonRepository Lessons { get; }
        IUserRepository Users { get; }
        IMaterialRepository Materials { get; }
        IMaterialStateRepository MaterialStates { get; }
        ISidebarRepository SidebarRepository { get; }
        IStatisticsRepository StatisticsRepository { get; }
        INotificationRepository NotificationRepository { get; }
        void SaveChanges();
    }
}