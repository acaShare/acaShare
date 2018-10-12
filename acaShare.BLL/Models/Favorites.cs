using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Favorites
    {
        public int UserId { get; set; }
        public int FileId { get; set; }

        public virtual Material File { get; set; }
        public virtual User User { get; set; }
    }
}
