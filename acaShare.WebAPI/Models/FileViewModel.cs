namespace acaShare.WebAPI.Models
{
    public class FileViewModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string RelativePath { get; set; }
        public bool IsImage => ContentType.StartsWith("image");
    }
}
