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
    public class SuggestionsService : ISuggestionsService
    {
        private readonly IMaterialsService _materialsService;
        private readonly IUserService _userService;
        private readonly IFormFilesManagement _filesManagement;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public SuggestionsService(
            IMaterialsService materialsService, 
            IUserService userService, 
            IFormFilesManagement filesManagement,
            NavigationManager navigationManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _materialsService = materialsService;
            _userService = userService;
            _filesManagement = filesManagement;
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public event Action OnEndProcessing;

        public async void OnApproveSuggestionClickCallback(CallbackArgs callbackArgs)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var loggedModerator = _userService.FindByIdentityUserId(authState.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var deleteRequest = _materialsService.GetDeleteRequest(callbackArgs.Id);
            if (deleteRequest == null)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.DeleteSuggestionNotFound}");
            }

            //_filesManagement.DeleteWholeMaterialFolder(deleteRequest.MaterialToDeleteId.Value);
            //_materialsService.ApproveDeleteRequest(deleteRequest, loggedModerator);

            _navigationManager.NavigateTo($"Moderator/ModeratorPanel/DeleteSuggestions");
           
            OnEndProcessing?.Invoke();
        }

        public async void OnRejectSuggestionClickCallback(RejectSuggestionCallbackArgs callbackArgs)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var loggedModerator = _userService.FindByIdentityUserId(authState.User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                //_materialsService.DeclineDeleteRequest(callbackArgs.Id, loggedModerator, callbackArgs.Reason);
            }
            catch (ArgumentException)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.DeleteSuggestionNotFound}");
            }

            _navigationManager.NavigateTo($"Moderator/ModeratorPanel/DeleteSuggestions");

            OnEndProcessing?.Invoke();
        }

        public void OnApproveEditSuggestionClickCallback(CallbackArgs callbackArgs)
        {
            var editRequest = _materialsService.GetEditRequest(callbackArgs.Id);
            if (editRequest == null)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.EditSuggestionNotFound}");
            }

            try
            {
                int materialToUpdateId = editRequest.MaterialToUpdateId;
                _materialsService.ApproveEditRequest(editRequest);
                _filesManagement.ReplaceMaterialFilesWithEditRequestFiles(materialToUpdateId, editRequest.EditRequestId, editRequest.Files);
            }
            catch (ArgumentException)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.EditSuggestionNotFound}");
            }
            catch (Exception)
            {
                _filesManagement.RemoveFilesFromFileSystem(editRequest.Files);
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.EditSuggestionNotFound}");
            }
        }

        public void OnRejectEditSuggestionClickCallback(RejectSuggestionCallbackArgs callbackArgs)
        {
            try
            {
                _materialsService.DeclineEditRequest(callbackArgs.Id, callbackArgs.Reason);
            }
            catch (ArgumentException)
            {
                _navigationManager.NavigateTo($"ResourceNotFound/{Errors.EditSuggestionNotFound}");
            }
        }
    }
}
