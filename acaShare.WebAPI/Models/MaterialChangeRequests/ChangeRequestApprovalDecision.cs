namespace acaShare.WebAPI.Models.MaterialChangeRequests
{
    public class ChangeRequestApprovalDecision
    {
        public MaterialToApproveViewModel MaterialViewModel { get; set; }
        public DeleteRequestViewModel DeleteRequestViewModel { get; set; }
        public EditRequestViewModel EditRequestViewModel { get; set; }
    }
}
