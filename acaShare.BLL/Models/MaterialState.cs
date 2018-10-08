using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class MaterialState
    {
        public MaterialState()
        {
            Materials = new HashSet<Material>();
        }
        
        public int StateId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<Material> Materials { get; set; }
    }
}
