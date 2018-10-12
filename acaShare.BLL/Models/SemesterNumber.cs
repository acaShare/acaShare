using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class SemesterNumber
    {
        public SemesterNumber()
        {
            Semester = new HashSet<Semester>();
        }

        public int SemesterNumberId { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Semester> Semester { get; set; }
    }
}
