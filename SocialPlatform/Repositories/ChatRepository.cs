using SocialNetworkingPlatform.Chats;
using SocialNetworkingPlatform.Interfaces;

namespace SocialNetworkingPlatform.Repositories
{
    /// <summary>
    /// Чатын санах ойн Repository
    /// </summary>
    public class ChatRepository : IChatRepository
    {
        private readonly List<IChat> _chats = new();

        /// <summary>Нэмэх</summary>
        public void Add(IChat chat) => _chats.Add(chat);

        /// <summary>Устгах</summary>
        public void Remove(IChat chat) => _chats.Remove(chat);

        /// <summary>ID-гаар хайх</summary>
        public IChat? GetById(Guid id) =>
            _chats.FirstOrDefault(c => c.Id == id);

        /// <summary>Бүгдийг авах</summary>
        public IReadOnlyList<IChat> GetAll() =>
            _chats.AsReadOnly();

        /// <summary>Хэрэглэгчийн чатуудыг авах</summary>
        public IEnumerable<IChat> GetByUser(Guid userId) =>
            _chats.Where(c => c is SingleChat sc && sc.HasUser(userId)
                           || c is GroupChat gc && gc.IsMember(userId));

        /// <summary>Хоёр хэрэглэгчийн SingleChat авах</summary>
        public IChat? GetSingleChat(Guid user1Id, Guid user2Id) =>
            _chats.FirstOrDefault(c => c is SingleChat sc
                && sc.HasUser(user1Id)
                && sc.HasUser(user2Id));
    }
}
