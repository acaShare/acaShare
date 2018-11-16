using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Favorites
    {
        public int UserId { get; set; }
        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }
        public virtual User User { get; set; }
    }
}
