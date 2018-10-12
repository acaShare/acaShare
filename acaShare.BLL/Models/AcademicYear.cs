using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            Semesters = new HashSet<Semester>();
        }

        public int AcademicYearId { get; set; }
        public DateTime YearFrom { get; set; }
        public DateTime YearTo { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
