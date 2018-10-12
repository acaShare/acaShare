using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class SemesterNumber
    {
        public SemesterNumber()
        {
            Semesters = new HashSet<Semester>();
        }

        public int SemesterNumberId { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
