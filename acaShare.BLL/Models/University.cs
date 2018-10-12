using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class University
    {
        public University()
        {
            Department = new HashSet<Department>();
            UserInUniversity = new HashSet<UserInUniversity>();
        }

        public int UniversityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<UserInUniversity> UserInUniversity { get; set; }
    }
}
