using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class File
    {
        public int FileId { get; set; }

        public int? MaterialId { get; set; }
        
        [Required]
        public byte[] FileContent { get; set; }

        public int? EditRequestId { get; set; }

        public EditRequest EditRequest { get; set; }

        public Material Material { get; set; }
    }
}
