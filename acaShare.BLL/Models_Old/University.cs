using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace acaShare.BLL.Models_Old
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
