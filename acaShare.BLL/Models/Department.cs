using System;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.BLL.Models
{
    public partial class Department
    {
        public Department(string name, string abbreviation, University university)
        {
            Update(name, abbreviation);
            University = university;
            UniversityId = university.UniversityId;
        }

        protected Department()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public int UniversityId { get; private set; }

        public virtual University University { get; private set; }
        public virtual ICollection<Lesson> Lessons { get; private set; }

        public void Update(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}

