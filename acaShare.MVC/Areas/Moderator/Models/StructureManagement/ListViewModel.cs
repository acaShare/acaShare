using acaShare.MVC.Areas.Universities.Models.Sidebar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.StructureManagement
{
    public class ListViewModel<IListItemViewModel>
    {
        public ICollection<IListItemViewModel> Items { get; set; }
    }
}
