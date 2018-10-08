using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class University
    {
        public University()
        {
            Departments = new HashSet<Department>();
            UsersParticipations = new HashSet<UserInUniversity>();
        }
        
        public int UniversityId { get; set; }

        [Required]
        [StringLength(126)]
        public string Name { get; set; }
        
        public ICollection<Department> Departments { get; set; }
        
        public ICollection<UserInUniversity> UsersParticipations { get; set; }
    }
}
