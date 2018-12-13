using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class DeleteRequest
    {
        public DeleteRequest(User deleter, Material materialToDelete, int reasonId, string additionalComment = null) : this()
        {
            if (reasonId <= 0)
            {
                throw new ArgumentException("ReasonId must be a positive integer");
            }

            RequestState = RequestState.PENDING;
            RequestDate = DateTime.Now;
            Deleter = deleter;
            MaterialToDelete = materialToDelete;
            materialToDelete.DeleteRequests.Add(this);
            DeleteReasonId = reasonId;
            AdditionalComment = additionalComment;
        }

        protected DeleteRequest()
        {
        }

        public int DeleteRequestId { get; private set; }
        public int DeleterId { get; private set; }
        public int? MaterialToDeleteId { get; private set; }
        public int DeleteReasonId { get; private set; }
        public int? ModeratorId { get; private set; }
        public DateTime RequestDate { get; private set; }
        public string AdditionalComment { get; private set; }
        public string DeclineReason { get; private set; }
        public RequestState RequestState { get; set; }

        public virtual User Deleter { get; private set; }
        public virtual User Moderator { get; private set; }
        public virtual Material MaterialToDelete { get; private set; }
        public virtual ChangeReason DeleteReason { get; private set; }

        public void ApproveRequest(User moderator)
        {
            RequestState = RequestState.APPROVED;
            Moderator = moderator;
            Deleter.Notify(
                NotificationType.DELETE_REQUEST_APPROVED,
                new Dictionary<string, string>
                {
                    { "MaterialName", MaterialToDelete.Name },
                    { "MaterialId", MaterialToDeleteId.ToString() }
                });
        }

        public void DeclineRequest(string declineReason, User moderator)
        {
            RequestState = RequestState.DECLINED;
            DeclineReason = declineReason;
            Moderator = moderator;
            Deleter.Notify(
                NotificationType.DELETE_REQUEST_DECLINED,
                new Dictionary<string, string>
                {
                    { "MaterialName", MaterialToDelete.Name },
                    { "DeclineReason", declineReason },
                    { "MaterialId", MaterialToDeleteId.ToString() }
                });
        }
    }

    public enum RequestState
    {
        PENDING, APPROVED, DECLINED
    }
}
