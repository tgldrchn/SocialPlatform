using DiscordApp.Cores;
using SocialNetworkingPlatform.Models;


namespace DiscordApp.Cores
{
    /// <summary>
    /// Discord хэрэглэгч.
    /// User классаас удамшина.
    /// </summary>
    public class DiscordUser : User
    {
        private readonly List<DiscordUser> _friends = new();

        private readonly List<Server> _servers = new();
        public string Discriminator { get; private set; }
        public string DiscordTag => $"{Username}#{Discriminator}";
        public IReadOnlyList<DiscordUser> Friends => _friends;
        public IReadOnlyList<Server> Servers => _servers;
        public int FriendCount => _friends.Count;

       
        public DiscordUser(string name, string username,
                           string email, string password,
                           DateTime dateOfBirth,
                           string discriminator = "0001")
            : base(name, username, email, password, dateOfBirth)
        {
            Discriminator = discriminator;
        }


        /// <summary>Найз нэмэх</summary>
        public void AddFriend(DiscordUser friend)
        {
            if (friend.Id == Id)
                throw new InvalidOperationException("Өөрөө өөрийгөө найз болгох боломжгүй.");

            if (!_friends.Contains(friend))
            {
                _friends.Add(friend);
                friend._friends.Add(this);
            }


        }

        /// <summary>Найз хасах</summary>
        public void RemoveFriend(DiscordUser friend)
        {
            _friends.Remove(friend);
            friend._friends.Remove(this);
        }

        /// <summary>Найз мөн эсэх</summary>
        public bool IsFriend(Guid friendId) {
            return _friends.Any(f => f.Id == friendId); 
        }

        /// <summary>Серверд нэгдэх</summary>
        
        public void JoinServer(Server server)
        {
            _servers.Add(server);
        }

        /// <summary>Серверээс гарах</summary>
        public void LeaveServer(Server server)
        {
            _servers.Remove(server);   
        }


        public override string ToString() => $"[DiscordUser] {DiscordTag} |Friend Count:  {FriendCount} | Server Count: {Servers.Count}";
    }
}
