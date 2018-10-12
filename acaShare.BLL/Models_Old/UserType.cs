using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models_Old
{
    public class UserType
    {
        public UserType()
        {
            Universities = new HashSet<UserInUniversity>();
        }
        
        public int TypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<UserInUniversity> Universities { get; set; }
    }
}
