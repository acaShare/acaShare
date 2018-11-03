using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class University
    {
        public University(string name, string abbreviation)
        {
            Update(name, abbreviation);
        }

        // Private constructor for EF only
        protected University()
        {
            Departments = new HashSet<Department>();
            UsersInUniversity = new HashSet<UserInUniversity>();
        }

        public int UniversityId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public virtual ICollection<Department> Departments { get; private set; }
        public virtual ICollection<UserInUniversity> UsersInUniversity { get; private set; }

        public void Update(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
