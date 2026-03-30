using DiscordApp.Channels;
using DiscordApp.Cores;
using DiscordApp.Enums;
using SocialNetworkingPlatform.Repositories;
using SocialNetworkingPlatform.Services;

namespace DiscordApp.Winforms
{
    /// <summary>
    /// MainFormPresenter нь View (MainForm) болон Model/Service хоорондын холбоос юм.
    /// Бүх бизнес логик энд бичигдэнэ.
    /// </summary>
    public class MainFormPresenter
    {
        // View (UI)
        private readonly IMainView _view;

        // Repository (data хадгалах хэсэг)
        private readonly UserRepository _userRepo = new();
        private readonly PostRepository _postRepo = new();
        private readonly ChatRepository _chatRepo = new();

        // Service (business logic)
        private readonly UserService _userService;
        private readonly ChatService _chatService;
        private readonly DiscordPlatform _platform;

        // Серверүүдийн жагсаалт
        private readonly List<Server> _servers = new();

        // Одоогийн хэрэглэгч, сервер, channel
        public DiscordUser? CurrentUser { get; private set; }
        public Server? CurrentServer { get; private set; }
        public TextChannel? CurrentChannel { get; private set; }

        /// <summary>
        /// Constructor - View-ийг авч service-уудыг initialize хийнэ
        /// </summary>
        public MainFormPresenter(IMainView view)
        {
            _view = view;

            _userService = new UserService(_userRepo);
            _chatService = new ChatService(_chatRepo);
            _platform = new DiscordPlatform("Discord", _userRepo, _postRepo);
        }

        /// <summary>
        /// Апп эхлэх үед дуудагдана.
        /// Demo хэрэглэгч үүсгээд login дэлгэц харуулна.
        /// </summary>
        public void Initialize()
        {
            SeedDemoData();
            _view.ShowLoginView();
        }

        /// <summary>
        /// Demo хэрэглэгчид үүсгэнэ (туршилтын өгөгдөл)
        /// </summary>
        private void SeedDemoData()
        {
            _platform.SignUp("Tuguldur", "tguldurr", "tuguldur@gmail.com", "pass123", new DateTime(2000, 1, 1));
            _platform.SignUp("Bold", "bold456", "bold@gmail.com", "pass456", new DateTime(1999, 5, 15));
            _platform.SignUp("Anu", "anuuu", "anu@gmail.com", "pass789", new DateTime(2001, 3, 20));
        }

        /// <summary>
        /// Хэрэглэгч login хийх
        /// </summary>
        public void Login()
        {
            var user = _userService.Login(_view.LoginEmail, _view.LoginPassword);

            // Login амжилтгүй бол
            if (user is not DiscordUser du)
            {
                _view.ShowError("И-мэйл эсвэл нууц үг буруу байна.");
                return;
            }

            // Login амжилттай бол
            CurrentUser = du;

            // Workspace (server, channel) үүсгэнэ
            BuildWorkspace();

            _view.SetTitle($"Discord — {CurrentUser.Username}");
            _view.ShowMainView();

            // UI-г refresh хийнэ
            RefreshAll();
        }

        /// <summary>
        /// Шинэ хэрэглэгч бүртгэх
        /// </summary>
        public void Register()
        {
            try
            {
                _platform.SignUp(
                    _view.RegisterName,
                    _view.RegisterUsername,
                    _view.RegisterEmail,
                    _view.RegisterPassword,
                    DateTime.Now.AddYears(-20));

                _view.ShowInfo("Амжилттай бүртгэгдлээ!");
                _view.ShowLoginView();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Login хийсний дараа анхны сервер, channel үүсгэнэ
        /// </summary>
        private void BuildWorkspace()
        {
            if (CurrentUser == null) return;

            _servers.Clear();

            // Шинэ сервер үүсгэнэ
            Server server = new Server("Mongolian Devs", CurrentUser);

            // Өөр хэрэглэгчийг хайж оруулах
            DiscordUser? user2 = _userRepo.GetAll()
                .OfType<DiscordUser>()
                .FirstOrDefault(u => u.Username == "bold456");

            if (user2 != null)
            {
                server.AddMember(user2, RoleType.Member);
            }

            // Channel-ууд нэмэх
            server.AddTextChannel("general");
            server.AddTextChannel("announcements");
            server.AddVoiceChannel("voice");

            _servers.Add(server);

            CurrentServer = server;
            CurrentChannel = server.Channels.OfType<TextChannel>().FirstOrDefault();

            // Demo message нэмэх
            if (CurrentChannel != null)
            {
                CurrentChannel.SendMessage(CurrentUser.Id, "Сайн байна уу!");

                if (user2 != null)
                {
                    CurrentChannel.SendMessage(user2.Id, "Сайн, сайн! 👋");
                }
            }
        }

        /// <summary>
        /// Шинэ сервер үүсгэх
        /// </summary>
        public void CreateServer(string name)
        {
            if (CurrentUser == null) return;
            if (string.IsNullOrWhiteSpace(name)) return;

            Server server = new Server(name, CurrentUser);
            server.AddTextChannel("general");

            _servers.Add(server);

            CurrentServer = server;
            CurrentChannel = server.Channels.OfType<TextChannel>().FirstOrDefault();

            RefreshAll();
        }

        /// <summary>
        /// Сервер сонгох
        /// </summary>
        public void SelectServer(Server server)
        {
            CurrentServer = server;
            CurrentChannel = server.Channels.OfType<TextChannel>().FirstOrDefault();

            RefreshAll();
        }

        /// <summary>
        /// Channel сонгох
        /// </summary>
        public void SelectChannel(TextChannel channel)
        {
            CurrentChannel = channel;
            RefreshMessagesOnly();
        }

        /// <summary>
        /// Message илгээх
        /// </summary>
        public void SendMessage()
        {
            if (CurrentUser == null) return;
            if (CurrentChannel == null) return;
            if (string.IsNullOrWhiteSpace(_view.MessageText)) return;

            CurrentChannel.SendMessage(CurrentUser.Id, _view.MessageText.Trim());

            // textbox-ийг цэвэрлэнэ
            _view.MessageText = "";

            RefreshMessagesOnly();
        }

        /// <summary>
        /// Бүх UI-г шинэчилнэ
        /// </summary>
        private void RefreshAll()
        {
            _view.RenderServers(_servers, CurrentServer);
            _view.RenderChannels(CurrentServer, CurrentChannel);
            _view.RenderMessages(CurrentChannel, ResolveUsername);
            _view.RenderMembers(CurrentServer, ResolveUsernameNullable);
        }

        /// <summary>
        /// Зөвхөн message хэсгийг шинэчилнэ
        /// </summary>
        private void RefreshMessagesOnly()
        {
            _view.RenderChannels(CurrentServer, CurrentChannel);
            _view.RenderMessages(CurrentChannel, ResolveUsername);
        }

        /// <summary>
        /// UserId-аас username олж буцаана
        /// </summary>
        private string ResolveUsername(Guid userId)
        {
            var user = _userRepo.GetById(userId);

            if (user != null)
            {
                return user.Username;
            }

            return "Unknown";
        }

        /// <summary>
        /// UserId-аас username олно (null байж болно)
        /// </summary>
        private string? ResolveUsernameNullable(Guid userId)
        {
            var user = _userRepo.GetById(userId);
            return user?.Username;
        }
    }
}