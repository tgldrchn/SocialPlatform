using DiscordApp.Channels;
using DiscordApp.Cores;
using DiscordApp.Enums;

namespace DiscordApp.Winforms
{
    /// <summary>
    /// IMainView интерфейс нь MainForm дээр байх ёстой
    /// property болон method-уудыг тодорхойлно.
    /// Presenter нь энэ интерфейсээр дамжиж View-тэй ажиллана.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Login хийх үед оруулсан email.
        /// </summary>
        string LoginEmail { get; }

        /// <summary>
        /// Login хийх үед оруулсан password.
        /// </summary>
        string LoginPassword { get; }

        /// <summary>
        /// Register хийх үед оруулсан нэр.
        /// </summary>
        string RegisterName { get; }

        /// <summary>
        /// Register хийх үед оруулсан username.
        /// </summary>
        string RegisterUsername { get; }

        /// <summary>
        /// Register хийх үед оруулсан email.
        /// </summary>
        string RegisterEmail { get; }

        /// <summary>
        /// Register хийх үед оруулсан password.
        /// </summary>
        string RegisterPassword { get; }

        /// <summary>
        /// Message input хэсгийн текст.
        /// Getter болон setter аль аль нь байна.
        /// </summary>
        string MessageText { get; set; }

        /// <summary>
        /// Login дэлгэцийг харуулна.
        /// </summary>
        void ShowLoginView();

        /// <summary>
        /// Register дэлгэцийг харуулна.
        /// </summary>
        void ShowRegisterView();

        /// <summary>
        /// Main chat дэлгэцийг харуулна.
        /// </summary>
        void ShowMainView();

        /// <summary>
        /// Form-ийн гарчгийг өөрчилнө.
        /// </summary>
        void SetTitle(string title);

        /// <summary>
        /// Алдааны мэдээлэл харуулна.
        /// </summary>
        void ShowError(string message);

        /// <summary>
        /// Энгийн мэдээлэл харуулна.
        /// </summary>
        void ShowInfo(string message);

        /// <summary>
        /// Серверүүдийн жагсаалтыг харуулна.
        /// </summary>
        void RenderServers(IEnumerable<Server> servers, Server? currentServer);

        /// <summary>
        /// Сонгогдсон серверийн channel-уудыг харуулна.
        /// </summary>
        void RenderChannels(Server? server, TextChannel? currentChannel);

        /// <summary>
        /// Сонгогдсон channel доторх message-үүдийг харуулна.
        /// resolveUsername функц нь user id-аар username олоход ашиглагдана.
        /// </summary>
        void RenderMessages(TextChannel? channel, Func<Guid, string> resolveUsername);

        /// <summary>
        /// Серверийн бүх member-ийг харуулна.
        /// resolveUsername функц нь user id-аар username олоход ашиглагдана.
        /// </summary>
        void RenderMembers(Server? server, Func<Guid, string?> resolveUsername);
    }
}