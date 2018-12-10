using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public class FavouriteMaterialViewModel : ISidebarContentViewModel
    {
        public string Content { get; set; }
        public int RouteValue { get; set; }
    }
}
