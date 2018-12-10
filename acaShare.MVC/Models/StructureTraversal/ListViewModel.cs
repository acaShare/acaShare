using acaShare.MVC.Areas.Main.Models.Sidebar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Models.StructureTraversal
{
    public class ListViewModel<TListItemViewModel>
    {
        public ICollection<TListItemViewModel> Items { get; set; }
        public bool IsWithSubtitles { get; set; }
        public int HelperId { get; set; }
    }
}
