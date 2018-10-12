using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int LecturerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
