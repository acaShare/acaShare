using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace acaShare.BLL.Models_Old
{
    public class Lesson
    {
        public Lesson()
        {
            Materials = new HashSet<Material>();
        }
        
        public int LessonId { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int SectionOfSubjectId { get; set; }
        public SectionOfSubject SectionOfSubject { get; set; }
        
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
