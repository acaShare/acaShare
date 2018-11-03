using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Models
{
    public class Breadcrumb
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }
}
