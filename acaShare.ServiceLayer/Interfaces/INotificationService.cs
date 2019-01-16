using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.ServiceLayer.Interfaces
{
    public interface INotificationService
    {
        ICollection<Notification> GetUserNotifications(int userId);
    }
}
