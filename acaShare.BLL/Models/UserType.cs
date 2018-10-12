using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class UserType
    {
        public UserType()
        {
            UsersInUniversity = new HashSet<UserInUniversity>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserInUniversity> UsersInUniversity { get; set; }
    }
}
