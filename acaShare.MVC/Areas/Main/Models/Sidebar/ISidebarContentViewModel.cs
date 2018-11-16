using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Models.Sidebar
{
    public interface ISidebarContentViewModel
    {
        string Content { get; set; }
        int RouteValue { get; set; }
    }
}
