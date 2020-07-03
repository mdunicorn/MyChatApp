using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Client.Services
{
    public interface IUserIdentificationService
    {
        Task<bool> IsAuthenticatedAsync();
        Task<string> GetUserNameAsync();
        bool IsGuestUser();
        Task<bool> IsGuestUserAsync();
        string GetGuestUserName();
        Task<string> GetGuestUserNameAsync();
        void SetGuestUserName(string name);
        Task SetGuestUserNameAsync(string name);
        Task ClearGuestUserAsync();

        event Action AuthenticationStateChanged;
    }
}
