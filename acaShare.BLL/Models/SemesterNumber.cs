using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
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
