using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class University
    {
        public University(string name, string abbreviation) : this()
        {
            Update(name, abbreviation);
        }

        // Private constructor for EF only
        protected University()
        {
            Departments = new HashSet<Department>();
            UsersInUniversity = new HashSet<UniversityMainModerator>();
        }

        public int UniversityId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public virtual ICollection<Department> Departments { get; private set; }
        public virtual ICollection<UniversityMainModerator> UsersInUniversity { get; private set; }

        public void Update(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
