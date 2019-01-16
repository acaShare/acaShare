using acaShare.BLL.Models;
using acaShare.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace acaShare.DAL.EFPersistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbSet<Notification> _dbSet;

        public NotificationRepository(DbSet<Notification> dbSet)
        {
            _dbSet = dbSet;
        }

        public ICollection<Notification> GetUserNotifications(int userId)
        {
            return _dbSet.AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(n => n.Date).ToList();
        }
    }
}
