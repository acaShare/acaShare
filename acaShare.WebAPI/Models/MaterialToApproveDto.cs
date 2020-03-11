namespace acaShare.WebAPI.Models
{
    public class MaterialToApproveDto
    {
        public int MaterialId { get; set; }

        public bool ShouldApprove { get; set; }
    }
}
