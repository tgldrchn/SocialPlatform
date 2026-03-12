
namespace SocialNetworkingPlatform.Chats
{
    /// <summary>
    /// Бүлгийн чат — олон хэрэглэгч оролцоно
    /// </summary>
    public class GroupChat : Chat
    {
        
        private readonly List<Guid> _memberIds = new();
        public string Name { get; private set; }
        public IReadOnlyList<Guid> MemberIds => _memberIds;
        public int MemberCount => _memberIds.Count;

     
        public GroupChat(string name, Guid creatorId)
        {
            Name = name;
            _memberIds.Add(creatorId);
        }


        /// <summary>Гишүүн нэмэх</summary>
        public void AddMember(Guid userId)
        {
            if (!_memberIds.Contains(userId))
                _memberIds.Add(userId);
        }

        /// <summary>Гишүүн хасах</summary>
        public void RemoveMember(Guid userId) =>
            _memberIds.Remove(userId);

        /// <summary>Гишүүн мөн эсэх</summary>
        public bool IsMember(Guid userId) =>
            _memberIds.Contains(userId);

        public override string ToString() =>
            $"[GroupChat] {Name} | Member Count: {MemberCount} | Message Count: {Messages.Count}";
    }
}
