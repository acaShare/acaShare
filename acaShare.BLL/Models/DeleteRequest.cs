using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class DeleteRequest
    {
        public DeleteRequest(User deleter, Material materialToDelete) : this()
        {
            Deleter = deleter;
            MaterialToDelete = materialToDelete;
            materialToDelete.DeleteRequests.Add(this);
        }

        protected DeleteRequest()
        {
        }

        public int DeleteRequestId { get; private set; }
        public int DeleterId { get; private set; }
        public int MaterialToDeleteId { get; private set; }
        public int Reason { get; private set; }

        public virtual User Deleter { get; private set; }
        public virtual Material MaterialToDelete { get; private set; }
    }
}
