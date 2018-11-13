using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Materials
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string IdentityUserId { get; set; }
    }
}
