using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}