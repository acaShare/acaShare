using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public class LastActivity
    {
        public string Username { get; set; }
        public string Content { get; set; }
        public Material Material { get; set; }
        public DateTime Date { get; set; }
        public LastActivityType ActivityType { get; set; }
    }

    public enum LastActivityType
    {
        COMMENT, MATERIAL_ADD
    }
}
