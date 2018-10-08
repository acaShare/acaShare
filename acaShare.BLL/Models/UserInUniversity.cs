using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class UserInUniversity
    {
        public DateTime JoinDate { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public University University { get; set; }
        public int UniversityId { get; set; }
        
        public int TypeId { get; set; }
        public UserType UserType { get; set; }
    }
}
