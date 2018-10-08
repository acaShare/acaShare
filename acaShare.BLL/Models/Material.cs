using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models
{
    public class Material
    {
        public Material()
        {
            Comments = new HashSet<Comment>();
            DeleteRequests = new HashSet<DeleteRequest>();
            EditRequests = new HashSet<EditRequest>();
            Files = new HashSet<File>();
        }
        
        public int MaterialId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public int? ApproverId { get; set; }
        public User Approver { get; set; }

        public int? UpdaterId { get; set; }
        public User Updater { get; set; }

        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<DeleteRequest> DeleteRequests { get; set; }
        
        public ICollection<EditRequest> EditRequests { get; set; }
        
        public ICollection<File> Files { get; set; }
        
        public int StateId { get; set; }
        public MaterialState MaterialState { get; set; }
    }
}
