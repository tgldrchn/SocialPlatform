

namespace SocialNetworkingPlatform.Chats
{
    /// <summary>
    /// Хоёр хэрэглэгчийн хооронд явагдах хувийн чат
    /// </summary>
    public class SingleChat : Chat
    {
        public Guid User1Id { get; private set; }
        public Guid User2Id { get; private set; }

        public SingleChat(Guid user1Id, Guid user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }

        /// <summary>Тухайн хэрэглэгч энэ чатад байгаа эсэх</summary>
        public bool HasUser(Guid userId) =>
            User1Id == userId || User2Id == userId;

        /// <summary>Ярилцагчийн ID авах</summary>
        public Guid GetOtherUserId(Guid userId) =>
            User1Id == userId ? User2Id : User1Id;

        public override string ToString() =>
            $"[SingleChat] {User1Id} -- {User2Id} | Message Count: {Messages.Count}";
    }
}
