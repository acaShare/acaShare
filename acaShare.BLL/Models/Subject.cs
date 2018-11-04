using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Subject
    {
        public Subject(string name, string abbreviation, Department department) : this()
        {
            Name = name;
            Abbreviation = abbreviation;
            SubjectDepartment subjectDepartment = new SubjectDepartment(this, department);
            SubjectDepartment.Add(subjectDepartment);
        }

        protected Subject()
        {
            SubjectDepartment = new HashSet<SubjectDepartment>();
        }

        public int SubjectId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public virtual ICollection<SubjectDepartment> SubjectDepartment { get; private set; }
    }
}
