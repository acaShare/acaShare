using System;

namespace acaShare.WebAPI.Models
{
    public class NotificationViewModel
    {
        public int? MaterialId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
