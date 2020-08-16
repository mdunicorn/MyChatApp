using Microsoft.AspNetCore.Mvc;
using MyChatApp.Server.Services;
using MyChatApp.Shared;
using MyChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Controllers
{
    [ApiController]
    [Route(RouteConstants.Messages)]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet(RouteConstants.Recent)]
        public async Task<IReadOnlyList<ChatMessageDTO>> GetRecent()
        {
            return await _messagesService.GetMessagesAsync(20);
        }
    }
}
