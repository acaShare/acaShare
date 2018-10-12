using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class MaterialState
    {
        public MaterialState()
        {
            Material = new HashSet<Material>();
        }

        public int StateId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Material { get; set; }
    }
}
