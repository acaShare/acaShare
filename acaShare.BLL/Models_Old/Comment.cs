using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acaShare.BLL.Models_Old
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [StringLength(512)]
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
