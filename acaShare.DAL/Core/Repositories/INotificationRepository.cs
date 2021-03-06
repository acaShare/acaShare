﻿using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.DAL.Core.Repositories
{
    public interface INotificationRepository
    {
        ICollection<Notification> GetUserNotifications(int userId);
    }
}
