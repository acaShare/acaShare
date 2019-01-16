using acaShare.BLL.Models;
using acaShare.DAL.Core;
using acaShare.DAL.EFPersistence;
using acaShare.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _uow;

        public NotificationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICollection<Notification> GetUserNotifications(int userId)
        {
            return _uow.NotificationRepository.GetUserNotifications(userId);
        }
    }
}
