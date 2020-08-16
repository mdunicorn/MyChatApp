using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace MyChatApp.Client.Services.ServiceImplementations
{
    public class UserIdentificationService : IUserIdentificationService
    {
        private readonly string _localStorageKey;
        private readonly ILocalStorageService _localStorageService;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public event Action AuthenticationStateChanged;


        public UserIdentificationService(AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService, ISyncLocalStorageService syncLocalStorageService, NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;

            _localStorageKey = navigationManager.BaseUri + "MyChatApp-GuestUserName"; ;

            _authenticationStateProvider.AuthenticationStateChanged += authenticationStateProvider_AuthenticationStateChanged;
        }

        private async void authenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            await ClearGuestUserAsync();
        }

        protected virtual void OnAuthenticationStateChanged()
        {
            AuthenticationStateChanged?.Invoke();
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User?.Identity.IsAuthenticated ?? false;
        }

        public async Task<string> GetUserNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User?.Identity.IsAuthenticated == true)
            {
                return authState.User.Identity.Name;
            }
            else
            {
                return await GetGuestUserNameAsync();
            }
        }

        public bool IsGuestUser()
        {
            return !string.IsNullOrEmpty(GetGuestUserName());
        }

        public async Task<bool> IsGuestUserAsync()
        {
            return !string.IsNullOrEmpty(await GetGuestUserNameAsync());
        }

        public string GetGuestUserName()
        {
            return _syncLocalStorageService.GetItem<string>(_localStorageKey);
        }

        public Task<string> GetGuestUserNameAsync()
        {
            return _localStorageService.GetItemAsync<string>(_localStorageKey).AsTask();
        }

        public void SetGuestUserName(string name)
        {
            _syncLocalStorageService.SetItem(_localStorageKey, name);
            OnAuthenticationStateChanged();
        }

        public async Task SetGuestUserNameAsync(string name)
        {
            await _localStorageService.SetItemAsync(_localStorageKey, name);
            OnAuthenticationStateChanged();
        }

        public void ClearGuestUser()
        {
            _syncLocalStorageService.RemoveItem(_localStorageKey);
            OnAuthenticationStateChanged();
        }

        public async Task ClearGuestUserAsync()
        {
            await _localStorageService.RemoveItemAsync(_localStorageKey);
            OnAuthenticationStateChanged();
        }
    }
}
