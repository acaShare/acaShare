﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.BLL.Models
{
    public partial class Material
    {
        public Material(string name, string description, Lesson lesson, User creator, MaterialState state) : this()
        {
            Name = name;
            Description = description;
            Lesson = lesson;
            Creator = creator;
            State = state;
            UploadDate = DateTime.Now;
        }

        protected Material()
        {
            Comments = new HashSet<Comment>();
            DeleteRequests = new HashSet<DeleteRequest>();
            EditRequests = new HashSet<EditRequest>();
            Favorites = new HashSet<Favorites>();
            Files = new HashSet<File>();
        }

        public bool ContainsFile(File file)
        {
            return Files.Any(f => f == file);
        }

        public void AddFile(File file)
        {
            if (file != null && !ContainsFile(file))
            {
                Files.Add(file);
                file.AddToMaterial(this);
            }
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int MaterialId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime UploadDate { get; private set; }
        public DateTime? ModificationDate { get; private set; }
        public int LessonId { get; private set; }
        public int StateId { get; private set; }
        public int CreatorId { get; private set; }
        public int? ApproverId { get; private set; }
        public int? UpdaterId { get; private set; }

        public virtual User Approver { get; private set; }
        public virtual User Creator { get; private set; }
        public virtual Lesson Lesson { get; private set; }
        public virtual MaterialState State { get; private set; }
        public virtual User Updater { get; private set; }
        public virtual ICollection<Comment> Comments { get; private set; }
        public virtual ICollection<DeleteRequest> DeleteRequests { get; private set; }
        public virtual ICollection<EditRequest> EditRequests { get; private set; }
        public virtual ICollection<Favorites> Favorites { get; private set; }
        public virtual ICollection<File> Files { get; private set; }
    }
}
