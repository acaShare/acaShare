using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            DeleteRequests = new HashSet<DeleteRequest>();
            EditRequests = new HashSet<EditRequest>();
            Favorites = new HashSet<Favorites>();
            ApprovedMaterials = new HashSet<Material>();
            CreatedMaterials = new HashSet<Material>();
            UpdatedMaterials = new HashSet<Material>();
            UsersInUniversity = new HashSet<UserInUniversity>();
        }
        
        public int UserId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string IdentityUserId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<DeleteRequest> DeleteRequests { get; set; }
        public virtual ICollection<EditRequest> EditRequests { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Material> ApprovedMaterials { get; set; }
        public virtual ICollection<Material> CreatedMaterials { get; set; }
        public virtual ICollection<Material> UpdatedMaterials { get; set; }
        public virtual ICollection<UserInUniversity> UsersInUniversity { get; set; }
    }
}
