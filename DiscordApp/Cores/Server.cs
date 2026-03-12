using DiscordApp.Channels;
using DiscordApp.Enums;
using DiscordApp.Interfaces;

namespace DiscordApp.Cores
{
    /// <summary>
    /// Discord сервер
    /// </summary>
    public class Server : IServer
    {
        private readonly Dictionary<DiscordUser, RoleType> _members = new();
        private readonly List<IChannel> _channels = new();
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DiscordUser Owner { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public IReadOnlyDictionary<DiscordUser, RoleType> Members => _members;
        public IReadOnlyList<IChannel> Channels => _channels;

    
        public Server(string name, DiscordUser owner)
        {
            Name = name;
            Owner = owner;
            _members[owner] = RoleType.Owner;
            owner.JoinServer(this);
        }

        public void AddMember(DiscordUser user, RoleType role = RoleType.Member)
        {
            if (!_members.ContainsKey(user))
            {
                _members[user] = role;
                user.JoinServer(this);
            }
        }

        /// <summary>Гишүүн хасах</summary>
        public void RemoveMember(DiscordUser user)
        {
            if (_members.ContainsKey(user))
            {
                _members.Remove(user);
                user.LeaveServer(this);
            }
        }

        /// <summary>Гишүүний Role авах</summary>
        public RoleType? GetRole(Guid userId)
        {
            var member = _members.Keys.FirstOrDefault(m => m.Id == userId);
            if (member != null)
                return _members[member];
            return null;
        }

        /// <summary>Role солих</summary>
        public void ChangeMemberRole(Guid userId, RoleType role)
        {
            var member = _members.Keys.FirstOrDefault(m => m.Id == userId);
            if (member != null)
                _members[member] = role;
        }

        /// <summary>TextChannel нэмэх</summary>
        public TextChannel AddTextChannel(string name)
        {
            var channel = new TextChannel(name);
            _channels.Add(channel);
            return channel;
        }

        /// <summary>VoiceChannel нэмэх</summary>
        public VoiceChannel AddVoiceChannel(string name)
        {
            var channel = new VoiceChannel(name);
            _channels.Add(channel);
            return channel;
        }

        /// <summary>Гишүүн мөн эсэх</summary>
        public bool IsMember(Guid userId)
        {
            return _members.Keys.Any(m => m.Id == userId);
        } 

        public override string ToString() => $"[Server] {Name} | Member count: {_members.Count} | Channel count: {_channels.Count}";
    }
}