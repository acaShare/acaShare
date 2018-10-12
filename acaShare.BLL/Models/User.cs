using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            DeleteRequest = new HashSet<DeleteRequest>();
            EditRequest = new HashSet<EditRequest>();
            Favorites = new HashSet<Favorites>();
            MaterialApprover = new HashSet<Material>();
            MaterialCreator = new HashSet<Material>();
            MaterialUpdater = new HashSet<Material>();
            UserInUniversity = new HashSet<UserInUniversity>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<DeleteRequest> DeleteRequest { get; set; }
        public virtual ICollection<EditRequest> EditRequest { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Material> MaterialApprover { get; set; }
        public virtual ICollection<Material> MaterialCreator { get; set; }
        public virtual ICollection<Material> MaterialUpdater { get; set; }
        public virtual ICollection<UserInUniversity> UserInUniversity { get; set; }
    }
}
