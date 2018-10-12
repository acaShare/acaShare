using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Material
    {
        public Material()
        {
            Comment = new HashSet<Comment>();
            DeleteRequest = new HashSet<DeleteRequest>();
            EditRequest = new HashSet<EditRequest>();
            Favorites = new HashSet<Favorites>();
            File = new HashSet<File>();
        }

        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int LessonId { get; set; }
        public int StateId { get; set; }
        public int CreatorId { get; set; }
        public int? ApproverId { get; set; }
        public int? UpdaterId { get; set; }

        public virtual User Approver { get; set; }
        public virtual User Creator { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual MaterialState State { get; set; }
        public virtual User Updater { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<DeleteRequest> DeleteRequest { get; set; }
        public virtual ICollection<EditRequest> EditRequest { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<File> File { get; set; }
    }
}
