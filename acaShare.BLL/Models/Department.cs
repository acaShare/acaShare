using System;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.BLL.Models
{
    public partial class Department
    {
        public Department(string name, string abbreviation, University university)
        {
            Update(name, abbreviation, university);
        }

        protected Department()
        {
            SubjectDepartment = new HashSet<SubjectDepartment>();
        }

        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public int UniversityId { get; private set; }

        public virtual University University { get; private set; }
        public virtual ICollection<SubjectDepartment> SubjectDepartment { get; private set; }

        public void Update(string name, string abbreviation, University university)
        {
            Name = name;
            Abbreviation = abbreviation;
            University = university;
        }
    }
}

