using acaShare.Blazor.Models;
using System;

namespace acaShare.Blazor.ApplicationServices.Interfaces
{
    public interface ISuggestionsService
    {
        void OnApproveSuggestionClickCallback(CallbackArgs callbackArgs);
        void OnRejectSuggestionClickCallback(RejectSuggestionCallbackArgs callbackArgs);
        void OnApproveEditSuggestionClickCallback(CallbackArgs callbackArgs);
        void OnRejectEditSuggestionClickCallback(RejectSuggestionCallbackArgs callbackArgs);
        event Action OnEndProcessing;
    }
}
