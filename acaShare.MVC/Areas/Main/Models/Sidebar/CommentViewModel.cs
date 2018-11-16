using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public class CommentViewModel : ISidebarContentViewModel
    {
        public string Content { get; set; }
        public string When { get; set; }
        public int RouteValue { get; set; }

        public int CommentId { get; set; }
        public string Author { get; set; }
    }
}
