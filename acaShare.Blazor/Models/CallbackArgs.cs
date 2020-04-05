namespace acaShare.Blazor.Models
{
    public class CallbackArgs
    {
        public int Id { get; }
        public bool ShouldRedirect { get; }

        public CallbackArgs(int id, bool shouldRedirect = false)
        {
            Id = id;
            ShouldRedirect = shouldRedirect;
        }
    }
}
