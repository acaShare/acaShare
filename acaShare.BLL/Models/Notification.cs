using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public class Notification
    {
        public Notification(string content, User user) : this()
        {
            Content = content;
            Date = DateTime.Now;
            User = user;
            IsRead = false;
        }

        protected Notification()
        {
        }

        public int NotificationId { get; private set; }
        public string Content { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsRead { get; private set; }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int? MaterialId { get; set; }
        public virtual Material Material { get; private set; }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }

    public enum NotificationType
    {
        DELETE_REQUEST_DECLINED, DELETE_REQUEST_APPROVED
    }
}
