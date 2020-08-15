using IdentityServer4.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;
using MyChatApp.Shared;
using MyChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();

        public async Task SendMessage(ChatMessageModel message)
        {
            string identityUserName = null;
            if (Context.User?.Identity.IsAuthenticated ?? false)
                identityUserName = Context.User.Identity.Name;
            if (string.IsNullOrEmpty(identityUserName))
                identityUserName = Context.UserIdentifier;
            if(!string.IsNullOrEmpty(identityUserName))
                message.UserName = identityUserName;
            await Clients.All.SendAsync(Messages.RECEIVE, message);
        }

        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!userLookup.ContainsKey(currentId))
            {
                userLookup.Add(currentId, username);
                await Clients.AllExcept(currentId).SendAsync(
                    Messages.RECEIVE,
                    new ChatMessageModel(username, $"{username} joined the chat"));
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            // try to get connection
            string id = Context.ConnectionId;
            if (!userLookup.TryGetValue(id, out string username))
                username = "[unknown]";

            userLookup.Remove(id);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(
                Messages.RECEIVE,
                new ChatMessageModel(username, $"{username} has left the chat"));
            await base.OnDisconnectedAsync(e);
        }
    }
}
