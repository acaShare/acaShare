using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class MaterialState
    {
        public MaterialState()
        {
            Materials = new HashSet<Material>();
        }

        public int StateId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
