using acaShare.Blazor.Models;
using System;

namespace acaShare.Blazor.ApplicationServices.Interfaces
{
    public interface IMaterialStateChangeService
    {
        void OnApproveMaterialClickCallback(MaterialCallbackArgs materialCallbackArgs);
        void OnRejectMaterialClickCallback(MaterialCallbackArgs materialCallbackArgs);
        event Action OnEndProcessing;
    }
}
