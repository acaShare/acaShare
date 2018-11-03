using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Semester
    {
        protected Semester()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int SemesterId { get; private set; }
        public string Number { get; private set; }

        public virtual ICollection<Lesson> Lessons { get; private set; }
    }
}
