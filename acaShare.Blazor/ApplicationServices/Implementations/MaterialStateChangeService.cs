using Microsoft.AspNetCore.Components;
using acaShare.ServiceLayer.Interfaces;
using acaShare.Blazor.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using acaShare.Blazor.Models;
using System.Security.Claims;
using acaShare.Blazor.ApplicationServices.Constants;
using System;

namespace acaShare.Blazor.ApplicationServices.Implementations
{
    public class MaterialStateChangeService : IMaterialStateChangeService
    {
        private IMaterialsService _materialsService;
        private IUserService _userService;
        private NavigationManager _navigationManager;
        private AuthenticationStateProvider _authenticationStateProvider;

        public MaterialStateChangeService(
            IMaterialsService materialsService, 
            IUserService userService, 
            NavigationManager navigationManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _materialsService = materialsService;
            _userService = userService;
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public event Action OnEndProcessing;

        public async void OnApproveMaterialClickCallback(CallbackArgs materialCallbackArgs)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var loggedUser = _userService.FindByIdentityUserId(authState.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var material = _materialsService.GetMaterialToApprove(materialCallbackArgs.Id);
            if (material == null)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.MaterialToApproveNotValid}");
            }

            _materialsService.ApproveMaterial(material, loggedUser);

            if (materialCallbackArgs.ShouldRedirect)
            {
                _navigationManager.NavigateTo($"Material/{materialCallbackArgs.Id}");
            }
            else
            {
                _navigationManager.NavigateTo($"Moderator/ModeratorPanel/MaterialsToApprove");
            }

            OnEndProcessing?.Invoke();
        }

        public void OnRejectMaterialClickCallback(CallbackArgs materialCallbackArgs)
        {
            var materialToReject = _materialsService.GetMaterialToApprove(materialCallbackArgs.Id);
            if (materialToReject == null)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.MaterialToApproveNotValid}");
            }

            //_filesManagement.DeleteWholeMaterialFolder(materialCallbackArgs.MaterialId);
            //_materialsService.RejectMaterial(materialToReject);

            _navigationManager.NavigateTo($"Moderator/ModeratorPanel/MaterialsToApprove");

            OnEndProcessing?.Invoke();
        }
    }
}
