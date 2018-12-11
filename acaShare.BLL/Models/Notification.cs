using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? MaterialId { get; set; }
        public virtual Material Material { get; set; }
    }
}
