using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Subject
    {
        public Subject()
        {
            SectionsOfSubject = new HashSet<SectionOfSubject>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SectionOfSubject> SectionsOfSubject { get; set; }
    }
}
