using System.Collections.Generic;

namespace acaShare.WebAPI.Models
{
    public class Breadcrumb
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }
}
