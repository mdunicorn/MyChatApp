using MyChatApp.Server.Models;
using MyChatApp.Server.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IBaseDbService _dbService;

        public UsersService(IBaseDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<ApplicationUser> GetUserAsync(string userId)
        {
            return _dbService.GetByIdAsync<ApplicationUser, string>(userId);
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            return _dbService.GetByIdAsync<ApplicationUser, string, string>(userId, u => u.UserName);
        }

        public Task<string> GetUserIdAsync(string userName)
        {
            return _dbService.GetFirstOrDefaultAsync<ApplicationUser, string>(
                u => u.UserName == userName,
                u => u.Id);
        }
    }
}
