using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.ServiceLayer.Models
{
    public class ValidationError
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string ValidationMessage { get; set; }
        public int Code { get; set; }
    }
}
