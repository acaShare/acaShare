using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public class SidebarViewModel
    {
        public ICollection<LastActivityViewModel> LastActivities { get; set; }
        public ICollection<FavouriteMaterialViewModel> Favourites { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        public int MaterialId { get; set; }
        public string NewComment { get; set; }
    }
}
