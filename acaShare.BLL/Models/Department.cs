using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Department
    {
        public Department()
        {
            Lesson = new HashSet<Lesson>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
    }
}
