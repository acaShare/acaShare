using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace acaShare.BLL.Models_Old
{
    public class Subject
    {
        public Subject()
        {
            SectionsOfSubject = new HashSet<SectionOfSubject>();
        }
        
        public int SubjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        public ICollection<SectionOfSubject> SectionsOfSubject { get; set; }
    }
}
