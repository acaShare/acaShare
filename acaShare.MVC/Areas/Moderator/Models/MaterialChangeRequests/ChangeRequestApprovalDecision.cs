using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Moderator.Models.MaterialChangeRequests
{
    public class ChangeRequestApprovalDecision
    {
        public MaterialToApproveViewModel MaterialViewModel { get; set; }
        public DeleteRequestViewModel DeleteRequestViewModel { get; set; }
        public EditRequestViewModel EditRequestViewModel { get; set; }
    }
}
