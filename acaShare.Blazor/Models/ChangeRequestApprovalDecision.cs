namespace acaShare.Blazor.Models
{
    public class ChangeRequestApprovalDecision
    {
        public MaterialToApproveViewModel Material { get; set; }
        public DeleteRequestViewModel DeleteSuggestion { get; set; }
        public EditRequestViewModel EditSuggestion { get; set; }
    }
}
