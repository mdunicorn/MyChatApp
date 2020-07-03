using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Client.Services
{
    public interface IGuestUserService
    {
        bool IsGuestUser { get; }
        string GuestUserName { get; }
        void SetGuestUserName(string name);
        void ClearGuestUser();
    }
}
