using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class Comment
    {
        protected Comment()
        {
        }

        public Comment(string newComment, User commentAuthor) : this()
        {
            Content = newComment;
            User = commentAuthor;
            CreatedDate = DateTime.Now;
        }

        public int CommentId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public int MaterialId { get; private set; }
        public virtual Material Material { get; private set; }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }
    }
}