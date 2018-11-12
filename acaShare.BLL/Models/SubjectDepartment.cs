using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class SubjectDepartment
    {
        public SubjectDepartment(Subject subject, Department department)
        {
            Subject = subject;
            Department = department;
        }

        protected SubjectDepartment()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int SubjectDepartmentId { get; private set; }
        public int SubjectId { get; private set; }
        public int DepartmentId { get; private set; }

        public virtual Department Department { get; private set; }
        public virtual Subject Subject { get; private set; }
        public virtual ICollection<Lesson> Lessons { get; private set; }
    }
}
