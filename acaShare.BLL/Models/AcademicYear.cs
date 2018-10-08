using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
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
