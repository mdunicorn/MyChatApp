using Microsoft.EntityFrameworkCore;
using MyChatApp.Server.Models;
using MyChatApp.Server.Services.Data;
using MyChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyChatApp.Server.Services.Implementations
{
    public class MessagesService : IMessagesService
    {
        private readonly IBaseDbService _dbService;
        private readonly IUsersService _usersService;

        public MessagesService(IBaseDbService dbService, IUsersService usersService)
        {
            _dbService = dbService;
            _usersService = usersService;
        }

        public Task<List<ChatMessageDTO>> GetMessagesAsync(DateTimeOffset since)
        {
            return _dbService
                .Query<ChatMessage>(m => m.DateTime >= since)
                .Order()
                .Project()
                .ToListAsync();
        }

        public Task<List<ChatMessageDTO>> GetMessagesAsync(int lastCount)
        {
            return _dbService
                .Query<ChatMessage>()
                .Last(lastCount)
                .Order()
                .Project()
                .ToListAsync();
        }

        public async Task<List<ChatMessageDTO>> GetMessagesAsync(string forUser, Expression<Func<ChatMessage, bool>> otherFilter)
        {
            var query = _dbService.Query(otherFilter);
            query = await query.ForUserAsync(forUser, _usersService);
            return await query
                .Order()
                .Project()
                .ToListAsync();
        }

        public Task<List<ChatMessageDTO>> GetMessagesAsync(string forUser, DateTimeOffset since)
        {
            return GetMessagesAsync(forUser, m => m.DateTime >= since);
        }

        public async Task<List<ChatMessageDTO>> GetMessagesAsync(string forUser, int lastCount)
        {
            var query = _dbService.Query<ChatMessage>();
            query = await query.ForUserAsync(forUser, _usersService);
            return await query
                .Last(lastCount)
                .Order()
                .Project(forUser)
                .ToListAsync();
        }

        public async Task InsertMessageAsync(ChatMessageDTO messageDto)
        {
            var userId = await _usersService.GetUserIdAsync(messageDto.UserName);
            string guestUserName = null;
            if (userId == null)
                guestUserName = messageDto.UserName;
            var message = messageDto.ToDataModel(userId, guestUserName);
            await _dbService.InsertAsync(message);
        }

    }

    internal static class MessageQueryExtensions
    {
        public static async Task<IQueryable<ChatMessage>> ForUserAsync(this IQueryable<ChatMessage> query, string userName, IUsersService usersService)
        {
            var userId = await usersService.GetUserIdAsync(userName);
            if (userId == null)
                return query.Where(u => u.GuestUserName == userName);
            else
                return query.Where(u => u.ApplicationUserId == userId);
        }

        public static IQueryable<ChatMessage> Order(this IQueryable<ChatMessage> query)
        {
            return query.OrderBy(m => m.DateTime).ThenBy(m => m.Id);
        }

        public static IQueryable<ChatMessage> OrderDescending(this IQueryable<ChatMessage> query)
        {
            return query.OrderByDescending(m => m.DateTime).ThenByDescending(m => m.Id);
        }

        public static IQueryable<ChatMessage> Last(this IQueryable<ChatMessage> query, int lastCount)
        {
            return query
                .OrderDescending()
                .Take(lastCount);
        }

    }
}
