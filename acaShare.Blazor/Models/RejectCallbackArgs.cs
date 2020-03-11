namespace acaShare.Blazor.Models
{
    public class RejectSuggestionCallbackArgs
    {
        public int Id { get; }
        public string Reason { get; }

        public RejectSuggestionCallbackArgs(int id, string reason)
        {
            Id = id;
            Reason = reason;
        }
    }
}
