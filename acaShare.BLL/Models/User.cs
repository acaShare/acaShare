using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            DeleteRequests = new HashSet<DeleteRequest>();
            EditRequests = new HashSet<EditRequest>();
            CreatedMaterials = new HashSet<Material>();
            UpdatedMaterials = new HashSet<Material>();
            ApprovedMaterials = new HashSet<Material>();
            UniversityParticipations = new HashSet<UserInUniversity>();
        }
        
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<DeleteRequest> DeleteRequests { get; set; }
        public ICollection<EditRequest> EditRequests { get; set; }
        
        public ICollection<Material> CreatedMaterials { get; set; }
        public ICollection<Material> UpdatedMaterials { get; set; }
        public ICollection<Material> ApprovedMaterials { get; set; }
        
        public ICollection<UserInUniversity> UniversityParticipations { get; set; }
    }
}
