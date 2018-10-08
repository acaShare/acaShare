using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class Semester
    {
        public Semester()
        {
            Lessons = new HashSet<Lesson>();
        }
        
        public int SemesterId { get; set; }

        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }

        public int SemesterNumberId { get; set; }
        public SemesterNumber SemesterNumber { get; set; }
        
        public ICollection<Lesson> Lessons { get; set; }

    }
}
