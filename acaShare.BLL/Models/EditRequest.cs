using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class EditRequest
    {
        protected EditRequest()
        {
            Files = new HashSet<File>();
        }

        public EditRequest(User updater, Material materialToUpdate, string summary, string newName, string newDescription) : this()
        {
            if (updater == null || materialToUpdate == null || string.IsNullOrEmpty(summary) || 
                string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException("Passed arguments are not valid for creation of a new Edit Request");
            }

            Updater = updater;
            MaterialToUpdate = materialToUpdate;
            RequestDate = DateTime.Now;
            Summary = summary;
            NewName = newName;
            NewDescription = newDescription;
        }

        public int EditRequestId { get; private set; }
        public int UpdaterId { get; private set; }
        public int MaterialToUpdateId { get; private set; }
        public DateTime RequestDate { get; private set; }
        public string Summary { get; private set; }
        public string NewName { get; private set; }
        public string NewDescription { get; private set; }

        public virtual Material MaterialToUpdate { get; private set; }
        public virtual User Updater { get; private set; }
        public virtual ICollection<File> Files { get; private set; }
        

        public ICollection<File> ApproveRequest(string materialsFolderName)
        {
            var filesToRemove = MaterialToUpdate.UpdateThroughEditRequest(this, materialsFolderName);
            
            MaterialToUpdate.Creator.Notify(
                NotificationType.UPDATE_REQUEST_APPROVED,
                new Dictionary<string, string>
                {
                    { "MaterialName", MaterialToUpdate.Name },
                    { "EditSummary", Summary },
                    { "MaterialId", MaterialToUpdateId.ToString() },
                    { "IsCreator", true.ToString() }
                });

            Updater.Notify(
                NotificationType.UPDATE_REQUEST_APPROVED,
                new Dictionary<string, string>
                {
                    { "MaterialName", MaterialToUpdate.Name },
                    { "EditSummary", Summary },
                    { "MaterialId", MaterialToUpdateId.ToString() }
                });

            MaterialToUpdate.RemoveEditRequests();

            return filesToRemove;
        }

        public void DeclineRequest(string declineReason)
        {
            Updater.Notify(
                NotificationType.UPDATE_REQUEST_DECLINED,
                new Dictionary<string, string>
                {
                    { "MaterialName", MaterialToUpdate.Name },
                    { "DeclineReason", declineReason },
                    { "MaterialId", MaterialToUpdateId.ToString() }
                });
        }

        public void AddFiles(ICollection<File> newFiles)
        {
            if (newFiles == null)
            {
                throw new ArgumentNullException("Provided files cannot be null");
            }

            foreach (var file in newFiles)
            {
                Files.Add(file);
            }
        }
    }
}

