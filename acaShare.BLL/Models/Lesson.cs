using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public class Lesson
    {
        public Lesson()
        {
            Materials = new HashSet<Material>();
        }

        public int LessonId { get; set; }
        public int SemesterId { get; set; }
        public int LecturerId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionOfSubjectId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual SectionOfSubject SectionOfSubject { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
