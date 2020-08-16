using MyChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Services
{
    public interface IMessagesService
    {
        Task<List<ChatMessageDTO>> GetMessagesAsync(DateTimeOffset since);
        Task<List<ChatMessageDTO>> GetMessagesAsync(int lastCount);
        Task<List<ChatMessageDTO>> GetMessagesAsync(string forUser, DateTimeOffset since);
        Task<List<ChatMessageDTO>> GetMessagesAsync(string forUser, int lastCount);
        Task InsertMessageAsync(ChatMessageDTO messageDto);
    }
}
