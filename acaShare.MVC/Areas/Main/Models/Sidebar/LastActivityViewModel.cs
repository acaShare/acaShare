using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public class LastActivityViewModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string When { get; set; }
        public LastActivityType Type { get; set; }
    }

    public enum LastActivityType
    {
        MATERIAL_ADD, COMMENT
    }
}
