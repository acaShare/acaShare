namespace acaShare.Blazor.Models
{
    public class MaterialCallbackArgs
    {
        public int MaterialId { get; }
        public bool ShouldRedirectToMaterial { get; }

        public MaterialCallbackArgs(int materialId, bool shouldRedirectToMaterial = false)
        {
            MaterialId = materialId;
            ShouldRedirectToMaterial = shouldRedirectToMaterial;
        }
    }
}
