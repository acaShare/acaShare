using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class University
    {
        public University()
        {
            Departments = new HashSet<Department>();
            UsersInUniversity = new HashSet<UserInUniversity>();
        }

        public int UniversityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<UserInUniversity> UsersInUniversity { get; set; }
    }
}
