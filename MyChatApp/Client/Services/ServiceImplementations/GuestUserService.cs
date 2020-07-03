using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace MyChatApp.Client.Services.ServiceImplementations
{
    public class GuestUserService : IGuestUserService
    {
        private readonly string _localStorageKey;
        private readonly ISyncLocalStorageService _localStorageService;

        public GuestUserService(ISyncLocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;

            _localStorageKey = navigationManager.BaseUri + "MyChatApp-GuestUserName";;
        }

        public bool IsGuestUser => !string.IsNullOrEmpty(GuestUserName);

        public string GuestUserName => _localStorageService.GetItem<string>(_localStorageKey);

        public void SetGuestUserName(string name)
        {
            _localStorageService.SetItem(_localStorageKey, name);
        }

        public void ClearGuestUser()
        {
            _localStorageService.RemoveItem(_localStorageKey);
        }
    }
}
