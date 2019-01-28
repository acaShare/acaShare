using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class UniversityMainModerator
    {
        public int UserId { get; set; }
        public int UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual User User { get; set; }
    }
}
