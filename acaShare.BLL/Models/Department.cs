using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class Department
    {
        public Department()
        {
            Lessons = new HashSet<Lesson>();
        }
        
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int UniversityId { get; set; }
        public University University { get; set; }
        
        public ICollection<Lesson> Lessons { get; set; }
    }
}
