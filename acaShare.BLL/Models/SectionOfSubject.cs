using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class SectionOfSubject
    {
        public SectionOfSubject()
        {
            Lesson = new HashSet<Lesson>();
        }

        public int SectionOfSubjectId { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
    }
}
