using MyChatApp.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Models
{
    public class ChatMessage : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public string ApplicationUserId { get; set; }
        public string GuestUserName { get; set; }

        public string Message { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
