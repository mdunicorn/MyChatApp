using MyChatApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Models
{
    public static class ModelMappingExtensions
    {
        public static IQueryable<ChatMessageDTO> Project(this IQueryable<ChatMessage> messages)
        {
            return messages.Select(
                message =>
                new ChatMessageDTO(
                    message.ApplicationUserId != null ? message.ApplicationUser.UserName : message.GuestUserName,
                    message.Message, message.DateTime));
        }

        public static IQueryable<ChatMessageDTO> Project(this IQueryable<ChatMessage> messages, string fixedUserName)
        {
            return messages.Select(
                message =>
                new ChatMessageDTO(fixedUserName, message.Message, message.DateTime));
        }

        public static ChatMessage ToDataModel(this ChatMessageDTO dto, string userId = null, string guestUserName = null)
        {
            if (userId == null && guestUserName == null)
                throw new ArgumentNullException(
                    $"'{nameof(userId)}' or '{nameof(guestUserName)}'",
                    "Specify exactly one of the parameters.");
            if (userId != null && guestUserName != null)
                throw new ArgumentException(
                    "Specify only one of the parameters!",
                    $"'{nameof(userId)}' or '{nameof(guestUserName)}'");

            return new ChatMessage()
            {
                ApplicationUserId = userId,
                GuestUserName = guestUserName,
                Message = dto.Message,
                DateTime = dto.DateTime
            };
        }
    }
}
