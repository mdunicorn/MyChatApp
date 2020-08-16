using MyChatApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Services
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetUserIdAsync(string userName);
    }
}
