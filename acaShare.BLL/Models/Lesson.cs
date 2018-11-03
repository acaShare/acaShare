using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public class Lesson
    {
        protected Lesson()
        {
            Materials = new HashSet<Material>();
        }

        public int LessonId { get; private set; }
        public int SemesterId { get; private set; }
        public int SubjectDepartmentId { get; private set; }

        public virtual Semester Semester { get; private set; }
        public virtual SubjectDepartment SubjectDepartment { get; private set; }
        public virtual ICollection<Material> Materials { get; private set; }
    }
}
