using Ecommerce.Dtos.Messages;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class MessagesMapper
    {
        public static Messages ToMessagesModel(this CreateMessage message)
        {
            return new Messages
            {
                content = message.content,
                CreatedAt = new DateOnly(),
                fileId = message.fileId,
                senderId = message.senderId,
                receiverId = message.receiverId,
            };
        }

        public static MessageDto ToMessageDto(this Messages message)
        {
            return new MessageDto
            {
                Id = message.Id,
                content = message.content,
                fileId = message.fileId,
                filePath = message.media.file,
                receiverId = message.receiverId,
                ReceiverName = message.receiver.UserName,
                senderId = message.senderId,
                SenderName = message.sender.UserName,
                CreatedAt = message.CreatedAt
            };
        }
    }
}
