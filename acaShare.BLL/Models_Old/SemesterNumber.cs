using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace acaShare.BLL.Models_Old
{
    public class SemesterNumber
    {
        public SemesterNumber()
        {
            Semesters = new HashSet<Semester>();
        }
        
        public int SemesterNumberId { get; set; }

        [Required]
        [StringLength(2)]
        public string Number { get; set; }
        
        public ICollection<Semester> Semesters { get; set; }
    }
}
