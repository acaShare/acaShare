using System;
using System.Collections.Generic;
using System.Text;

namespace acaShare.BLL.Models
{
    public class ChangeReason
    {
        public int ChangeReasonId { get; set; }
        public string Reason { get; set; }
        public ChangeType ChangeType { get; set; }
    }

    public enum ChangeType
    {
        EDIT, DELETE
    }
}
