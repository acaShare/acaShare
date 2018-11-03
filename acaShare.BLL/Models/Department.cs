using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Department
    {
        public Department(string name, University university)
        {
            Update(name, university);
        }

        protected Department()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int DepartmentId { get; private set; }
        public string Name { get; private set; }
        public int UniversityId { get; private set; }

        public virtual University University { get; private set; }
        public virtual ICollection<Lesson> Lessons { get; private set; }

        public void Update(string name, University university)
        {
            Name = name;
            University = university;
        }
    }
}
