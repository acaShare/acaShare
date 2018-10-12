using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models_Old
{
    public class AcademicYear
    {
        public AcademicYear()
        {
            Semesters = new HashSet<Semester>();
        }
        
        public int AcademicYearId { get; set; }
        public DateTime YearFrom { get; set; }
        public DateTime YearTo { get; set; }
        public ICollection<Semester> Semesters { get; set; }
    }
}
