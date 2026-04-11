using SocialNetworkingPlatform.Chats;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepo;

        public ChatService(IChatRepository chatRepo)
        {
            _chatRepo = chatRepo;
        }

        /// <summary>SingleChat үүсгэх</summary>
        public IChat CreateSingleChat(Guid user1Id, Guid user2Id)
        {
            var chat = new SingleChat(user1Id, user2Id);
            _chatRepo.Add(chat);
            return chat;
        }

        /// <summary>GroupChat үүсгэх</summary>
        public IChat CreateGroupChat(Guid creatorId, string name)
        {
            var chat = new GroupChat(name, creatorId);
            _chatRepo.Add(chat);
            return chat;
        }

        /// <summary>Мессеж илгээх</summary>
        public IMessage SendMessage(Guid chatId, Guid senderId, string content, IAttachment ?attachment = null)
        {
            var chat = _chatRepo.GetById(chatId)
                ?? throw new Exception("Чат олдсонгүй.");
            return chat.SendMessage(senderId, content, attachment);
        }

        /// <summary>Мессежүүд авах</summary>
        public IReadOnlyList<IMessage> GetMessages(Guid chatId)
        {
            var chat = _chatRepo.GetById(chatId)
                ?? throw new Exception("Чат олдсонгүй.");
            return chat.Messages;
        }
    }
}