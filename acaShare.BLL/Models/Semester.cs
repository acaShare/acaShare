using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int SemesterId { get; set; }
        public int AcademicYearId { get; set; }
        public int SemesterNumberId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual SemesterNumber SemesterNumber { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
