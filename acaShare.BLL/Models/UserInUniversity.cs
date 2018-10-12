using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class UserInUniversity
    {
        public DateTime JoinDate { get; set; }
        public int UserId { get; set; }
        public int UniversityId { get; set; }
        public int TypeId { get; set; }

        public virtual UserType Type { get; set; }
        public virtual University University { get; set; }
        public virtual User User { get; set; }
    }
}
