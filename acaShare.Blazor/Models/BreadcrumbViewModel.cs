namespace acaShare.Blazor.Models
{
    public class BreadcrumbViewModel
    {
        public BreadcrumbViewModel(string linkText, string href = null)
        {
            LinkText = linkText;
            Href = href;
        }

        public string LinkText { get; }
        public string Href { get; }
    }
}
