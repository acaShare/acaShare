using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class SectionOfSubject
    {
        public SectionOfSubject()
        {
            Lessons = new HashSet<Lesson>();
        }
        
        public int SectionOfSubjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<Lesson> Lessons { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
