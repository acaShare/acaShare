using Microsoft.AspNetCore.Components;

namespace acaShare.Blazor.Components
{
    public class RedirectToLogin : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo("Identity/Account/WelcomePage", true);
        }
    }
}
