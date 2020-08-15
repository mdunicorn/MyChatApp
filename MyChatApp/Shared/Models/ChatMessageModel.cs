using System;
using System.Collections.Generic;
using System.Text;

namespace MyChatApp.Shared.Models
{
    public class ChatMessageModel
    {
        public ChatMessageModel()
        { }

        public ChatMessageModel(string username, string message, DateTimeOffset? dateTime = null)
        {
            UserName = username;
            Message = message;
            DateTime = dateTime ?? DateTimeOffset.Now;
        }

        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
