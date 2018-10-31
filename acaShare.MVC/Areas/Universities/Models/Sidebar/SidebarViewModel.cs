using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Universities.Models.Sidebar
{
    public class SidebarViewModel
    {
        public ICollection<LastActivityVM> LastActivities { get; set; }
        public ICollection<FavouriteMaterialsVM> Favourites { get; set; }
    }
}
