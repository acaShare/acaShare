namespace acaShare.Blazor.Models
{
    public class EditRequestCallbackArgs
    {
        public int EditRequestId { get; }
        public bool ShouldRedirectToMaterial { get; }

        public EditRequestCallbackArgs(int editRequestId, bool shouldRedirectToMaterial = false)
        {
            EditRequestId = editRequestId;
            ShouldRedirectToMaterial = shouldRedirectToMaterial;
        }
    }
}
