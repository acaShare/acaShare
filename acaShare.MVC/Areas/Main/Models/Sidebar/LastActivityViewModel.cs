using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public class LastActivityViewModel : ISidebarContentViewModel
    {
        public string Who { get; set; }
        public string Content { get; set; }
        public string When { get; set; }
        public int RouteValue { get; set; }

        public LastActivityType Type { get; set; }
        public Material Material { get; set; }
    }
}
