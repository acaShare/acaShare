namespace acaShare.WebAPI.Models.MaterialChangeRequests
{
    public class DeleteRequestApprovalDecisionDto
    {
        public int DeleteRequestId { get; set; }
        public string DeclineReason { get; set; }
        public bool ShouldApprove { get; set; }
    }
}
