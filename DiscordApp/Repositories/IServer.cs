using DiscordApp.Channels;
using DiscordApp.Cores;
using DiscordApp.Enums;

namespace DiscordApp.Interfaces
{
    /// <summary>
    /// Серверийн интерфэйс
    /// </summary>
    public interface IServer
    {
        Guid Id { get; }
        string Name { get; }
        DiscordUser Owner { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<IChannel> Channels { get; }
        IReadOnlyDictionary<DiscordUser, RoleType> Members { get; }
        void AddMember(DiscordUser user, RoleType role);
        void RemoveMember(DiscordUser user);
        RoleType? GetRole(Guid userId);
        TextChannel AddTextChannel(string name);
        VoiceChannel AddVoiceChannel(string name);
    }
}
