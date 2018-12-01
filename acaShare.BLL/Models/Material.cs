using System;
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

        public int FilesCount => Files.Count;

        public void Update(string name, string description, ICollection<File> newFiles, User updater)
        {
            Name = name;
            Description = description;
            ModificationDate = DateTime.Now;
            Updater = updater;
            AddFiles(newFiles);
        }

        public bool IsUserAllowedToEditOrDelete(User loggedUser)
        {
            return loggedUser.IdentityUserId == this.Creator.IdentityUserId;
        }

        public void UpdateState(int newStateId)
        {
            StateId = newStateId;
        }

        public void AddFiles(ICollection<File> filesToAdd)
        {
            foreach (var file in filesToAdd)
            {
                AddFile(file);
            }
        }

        public ICollection<File> UpdateExistingFilesAndGetFilesToRemove(ICollection<File> files)
        {
            var newFiles = Files.Where(existingFile => files.Select(f => f.FileId).Contains(existingFile.FileId)).ToList();
            var filesToRemove = Files.Where(existingFile => !newFiles.Select(f => f.FileId).Contains(existingFile.FileId)).ToList();
            
            foreach (var existingFile in newFiles)
            {
                var newFile = files.FirstOrDefault(f => f.FileId == existingFile.FileId);

                if (newFile != null)
                {
                    existingFile.Update(newFile.FileName);
                }
            }

            Files = newFiles;
            
            return filesToRemove;
        }
    }
}
