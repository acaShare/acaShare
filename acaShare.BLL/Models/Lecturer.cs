using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            Lessons = new HashSet<Lesson>();
        }
        
        public int LecturerId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        
        public ICollection<Lesson> Lessons { get; set; }
    }
}
