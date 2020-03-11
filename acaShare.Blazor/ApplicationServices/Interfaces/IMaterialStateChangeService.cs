using acaShare.Blazor.Models;
using System;

namespace acaShare.Blazor.ApplicationServices.Interfaces
{
    public interface IMaterialStateChangeService
    {
        void OnApproveMaterialClickCallback(CallbackArgs materialCallbackArgs);
        void OnRejectMaterialClickCallback(CallbackArgs materialCallbackArgs);
        event Action OnEndProcessing;
    }
}
