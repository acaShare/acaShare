using acaShare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.WebAPI.ViewModels
{
    public class MainPanelViewModel
    {
        public IEnumerable<University> Universities { get; set; }
    }
}
