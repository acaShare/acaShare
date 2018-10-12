using System;
using System.Collections.Generic;

namespace acaShare.BLL.Models
{
    public partial class DeleteRequest
    {
        public int DeleteRequestId { get; set; }
        public int DeleterId { get; set; }
        public int MaterialToDeleteId { get; set; }
        public int Reason { get; set; }

        public virtual User Deleter { get; set; }
        public virtual Material MaterialToDelete { get; set; }
    }
}
