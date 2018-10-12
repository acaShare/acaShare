using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models_Old
{
    public class EditRequest
    {
        public EditRequest()
        {
            Files = new HashSet<File>();
        }
        
        public int EditRequestId { get; set; }
        
        public DateTime RequestDate { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        [StringLength(255)]
        public string NewName { get; set; }

        [StringLength(4000)]
        public string NewDescription { get; set; }

        public int MaterialToUpdateId { get; set; }
        public Material Material { get; set; }

        public int UpdaterId { get; set; }
        public User User { get; set; }
        
        public ICollection<File> Files { get; set; }
    }
}
