using DiscordApp.Channels;
using DiscordApp.Cores;
using DiscordApp.Enums;
using DiscordApp.Winforms.Controls;
using System.ComponentModel;

namespace DiscordApp.Winforms
{
    /// <summary>
    /// Discord төрлийн програмын үндсэн form.
    /// Энэ класс нь login, register, main chat view-г харуулна.
    /// Мөн server, channel, message, member мэдээллийг UI дээр дүрслэнэ.
    /// </summary>
    public partial class MainForm : Form, IMainView
    {
        // Presenter объект. UI event-үүдийг presenter рүү дамжуулна.
        private MainFormPresenter _presenter;

        // UI дээр ашиглах үндсэн өнгөнүүд
        private static readonly Color DarkBg = Color.FromArgb(54, 57, 63);
        private static readonly Color DarkerBg = Color.FromArgb(47, 49, 54);
        private static readonly Color DarkestBg = Color.FromArgb(32, 34, 37);
        private static readonly Color Blurple = Color.FromArgb(88, 101, 242);
        private static readonly Color TextLight = Color.FromArgb(220, 221, 222);
        private static readonly Color TextMuted = Color.FromArgb(114, 118, 125);

        /// <summary>
        /// MainForm үүсэх үед component, presenter, event, color тохиргоог хийнэ.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _presenter = new MainFormPresenter(this);

            // Design mode биш үед л event болон initialize хийнэ
            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                WireEvents();
                ApplyColors();
                Load += MainForm_Load;
            }
        }

        /// <summary>
        /// Form ачаалагдах үед presenter initialize хийнэ.
        /// </summary>
        private void MainForm_Load(object? sender, EventArgs e)
        {
            _presenter.Initialize();
        }

        /// <summary>
        /// Form дээрх бүх event-үүдийг холбоно.
        /// </summary>
        private void WireEvents()
        {
            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;
            btnSend.Click += BtnSend_Click;

            lnkGoRegister.Click += LnkGoRegister_Click;
            lnkBackToLogin.Click += LnkBackToLogin_Click;

            txtLoginPassword.KeyDown += TxtLoginPassword_KeyDown;
            txtMessageInput.KeyDown += TxtMessageInput_KeyDown;
        }

        /// <summary>
        /// Login товч дарахад login ажиллагааг эхлүүлнэ.
        /// </summary>
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            _presenter.Login();
        }

        /// <summary>
        /// Register товч дарахад бүртгэлийн ажиллагааг эхлүүлнэ.
        /// </summary>
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            _presenter.Register();
        }

        /// <summary>
        /// Send товч дарахад мессеж илгээнэ.
        /// </summary>
        private void BtnSend_Click(object? sender, EventArgs e)
        {
            _presenter.SendMessage();
        }

        /// <summary>
        /// Register link дарахад register дэлгэцийг харуулна.
        /// </summary>
        private void LnkGoRegister_Click(object? sender, EventArgs e)
        {
            ShowRegisterView();
        }

        /// <summary>
        /// Back to login link дарахад login дэлгэцийг харуулна.
        /// </summary>
        private void LnkBackToLogin_Click(object? sender, EventArgs e)
        {
            ShowLoginView();
        }

        /// <summary>
        /// Login password textbox дээр Enter дарахад login хийнэ.
        /// </summary>
        private void TxtLoginPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _presenter.Login();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Message textbox дээр Enter дарахад мессеж илгээнэ.
        /// </summary>
        private void TxtMessageInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _presenter.SendMessage();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Form доторх panel, textbox, button, label-ийн өнгийг тохируулна.
        /// </summary>
        private void ApplyColors()
        {
            BackColor = DarkestBg;

            panelLogin.BackColor = DarkestBg;
            panelRegister.BackColor = DarkestBg;
            panelMain.BackColor = DarkestBg;

            panelServers.BackColor = DarkestBg;
            panelChannels.BackColor = DarkerBg;
            panelMembers.BackColor = DarkerBg;
            panelChat.BackColor = DarkBg;
            panelChatHeader.BackColor = DarkBg;
            panelChatInput.BackColor = DarkBg;
            flpMessages.BackColor = DarkBg;

            Color inputBg = Color.FromArgb(64, 68, 75);

            txtLoginEmail.BackColor = inputBg;
            txtLoginPassword.BackColor = inputBg;
            txtRegisterName.BackColor = inputBg;
            txtRegisterUsername.BackColor = inputBg;
            txtRegisterEmail.BackColor = inputBg;
            txtRegisterPassword.BackColor = inputBg;
            txtMessageInput.BackColor = inputBg;

            txtLoginEmail.ForeColor = TextLight;
            txtLoginPassword.ForeColor = TextLight;
            txtRegisterName.ForeColor = TextLight;
            txtRegisterUsername.ForeColor = TextLight;
            txtRegisterEmail.ForeColor = TextLight;
            txtRegisterPassword.ForeColor = TextLight;
            txtMessageInput.ForeColor = TextLight;

            btnLogin.BackColor = Blurple;
            btnLogin.ForeColor = Color.White;

            btnRegister.BackColor = Color.FromArgb(59, 165, 93);
            btnRegister.ForeColor = Color.White;

            btnSend.BackColor = Blurple;
            btnSend.ForeColor = Color.White;

            lnkGoRegister.LinkColor = Blurple;
            lnkBackToLogin.LinkColor = Blurple;

            lblLoginTitle.ForeColor = TextLight;
            lblRegisterTitle.ForeColor = TextLight;
        }

        /// <summary>
        /// Login хэсгийн email textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LoginEmail
        {
            get { return txtLoginEmail.Text; }
        }

        /// <summary>
        /// Login хэсгийн password textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LoginPassword
        {
            get { return txtLoginPassword.Text; }
        }

        /// <summary>
        /// Register хэсгийн name textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RegisterName
        {
            get { return txtRegisterName.Text; }
        }

        /// <summary>
        /// Register хэсгийн username textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RegisterUsername
        {
            get { return txtRegisterUsername.Text; }
        }

        /// <summary>
        /// Register хэсгийн email textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RegisterEmail
        {
            get { return txtRegisterEmail.Text; }
        }

        /// <summary>
        /// Register хэсгийн password textbox-ийн утгыг авна.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RegisterPassword
        {
            get { return txtRegisterPassword.Text; }
        }

        /// <summary>
        /// Message input textbox-ийн утгыг авах болон өөрчлөхөд ашиглана.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MessageText
        {
            get { return txtMessageInput.Text; }
            set { txtMessageInput.Text = value; }
        }

        /// <summary>
        /// Login panel-ийг харуулна.
        /// </summary>
        public void ShowLoginView()
        {
            panelLogin.Visible = true;
            panelRegister.Visible = false;
            panelMain.Visible = false;
            panelLogin.BringToFront();
        }

        /// <summary>
        /// Register panel-ийг харуулна.
        /// </summary>
        public void ShowRegisterView()
        {
            panelLogin.Visible = false;
            panelRegister.Visible = true;
            panelMain.Visible = false;
            panelRegister.BringToFront();
        }

        /// <summary>
        /// Main chat panel-ийг харуулна.
        /// </summary>
        public void ShowMainView()
        {
            panelLogin.Visible = false;
            panelRegister.Visible = false;
            panelMain.Visible = true;
            panelMain.BringToFront();
        }

        /// <summary>
        /// Form-ийн title-г өөрчилнө.
        /// </summary>
        public void SetTitle(string title)
        {
            Text = title;
        }

        /// <summary>
        /// Алдааны message box харуулна.
        /// </summary>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Алдаа");
        }

        /// <summary>
        /// Мэдээллийн message box харуулна.
        /// </summary>
        public void ShowInfo(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Серверүүдийг зүүн талын panel дээр button байдлаар харуулна.
        /// </summary>
        public void RenderServers(IEnumerable<Server> servers, Server? currentServer)
        {
            panelServers.Controls.Clear();
            panelServers.Padding = new Padding(12, 12, 12, 0);

            foreach (Server server in servers)
            {
                Server selectedServer = server;

                // Серверийн нэрнээс эхний 2 үсгийг авч button дээр харуулна
                string shortName = server.Name.Substring(0, Math.Min(2, server.Name.Length)).ToUpper();

                Button btn = CreateRoundButton(shortName, Blurple);
                btn.Margin = new Padding(0, 0, 0, 8);

                btn.Click += (s, e) =>
                {
                    _presenter.SelectServer(selectedServer);
                };

                panelServers.Controls.Add(btn);
            }

            // Шинэ сервер үүсгэх button
            Button addBtn = CreateRoundButton("+", Color.FromArgb(59, 165, 93));
            addBtn.Font = new Font("Segoe UI", 18);

            addBtn.Click += (s, e) =>
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Серверийн нэр:",
                    "Сервер үүсгэх"
                );

                _presenter.CreateServer(name);
            };

            panelServers.Controls.Add(addBtn);
        }

        /// <summary>
        /// Сонгогдсон серверийн channel-уудыг харуулна.
        /// </summary>
        public void RenderChannels(Server? server, TextChannel? currentChannel)
        {
            panelChannels.Controls.Clear();
            panelChatHeader.Controls.Clear();

            if (server == null)
            {
                return;
            }

            // Серверийн нэрийг channel panel дээр харуулна
            Label serverLabel = CreateLabel(server.Name, 14, TextLight, FontStyle.Bold);
            serverLabel.Dock = DockStyle.Top;
            serverLabel.Height = 48;
            serverLabel.Padding = new Padding(16, 0, 0, 0);
            serverLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelChannels.Controls.Add(serverLabel);

            AddSection("# TEXT CHANNELS");

            foreach (TextChannel channel in server.Channels.OfType<TextChannel>())
            {
                TextChannel selectedChannel = channel;

                AddChannelButton(
                    "#  " + channel.Name,
                    channel == currentChannel,
                    () => _presenter.SelectChannel(selectedChannel)
                );
            }

            AddSection("🔊 VOICE CHANNELS");

            foreach (VoiceChannel channel in server.Channels.OfType<VoiceChannel>())
            {
                AddChannelButton("🔈  " + channel.Name, false, () => { });
            }

            string headerText = "";
            if (currentChannel != null)
            {
                headerText = "#  " + currentChannel.Name;
            }

            // Chat header дээр одоогийн channel-ийн нэрийг харуулна
            Label header = CreateLabel(headerText, 15, TextLight, FontStyle.Bold);
            header.Dock = DockStyle.Left;
            header.Width = 300;
            header.Padding = new Padding(16, 0, 0, 0);
            header.TextAlign = ContentAlignment.MiddleLeft;
            panelChatHeader.Controls.Add(header);
        }

        /// <summary>
        /// Channel доторх бүх мессежийг message panel дээр харуулна.
        /// </summary>
        public void RenderMessages(TextChannel? channel, Func<Guid, string> resolveUsername)
        {
            flpMessages.Controls.Clear();

            if (channel == null)
            {
                return;
            }

            foreach (var message in channel.Messages)
            {
                string username = resolveUsername(message.SenderId);
                AddMessageCard(username, message.Content);
            }
        }

        /// <summary>
        /// Серверийн бүх member-ийг баруун талын panel дээр харуулна.
        /// </summary>
        public void RenderMembers(Server? server, Func<Guid, string?> resolveUsername)
        {
            panelMembers.Controls.Clear();

            if (server == null)
            {
                return;
            }

            Label title = CreateLabel("ГИШҮҮД — " + server.Members.Count, 11, TextMuted);
            title.Dock = DockStyle.Top;
            title.Height = 36;
            title.Padding = new Padding(12, 12, 0, 0);
            title.TextAlign = ContentAlignment.TopLeft;
            panelMembers.Controls.Add(title);

            foreach (var member in server.Members)
            {
                var user = member.Key;
                var role = member.Value;

                string? username = resolveUsername(user.Id);

                if (username == null)
                {
                    continue;
                }

                Panel row = new Panel();
                row.Dock = DockStyle.Top;
                row.Height = 44;
                row.Cursor = Cursors.Hand;
                row.Padding = new Padding(8, 4, 8, 0);

                // Mouse дээр очих үед background өнгө өөрчлөгдөнө
                row.MouseEnter += (s, e) =>
                {
                    row.BackColor = Color.FromArgb(57, 60, 67);
                };

                row.MouseLeave += (s, e) =>
                {
                    row.BackColor = Color.Transparent;
                };

                // Member-ийн avatar
                Label avatar = new Label();
                avatar.Text = username[0].ToString().ToUpper();
                avatar.Size = new Size(32, 32);
                avatar.Location = new Point(8, 6);
                avatar.TextAlign = ContentAlignment.MiddleCenter;
                avatar.BackColor = GetAvatarColor(username);
                avatar.ForeColor = Color.White;
                avatar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                avatar.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 32, 32, 16, 16));

                // Username
                Label lblName = CreateLabel(username, 13, TextLight);
                lblName.Location = new Point(46, 6);
                lblName.AutoSize = true;

                // Role
                Label lblRole = CreateLabel(role.ToString(), 11, TextMuted);
                lblRole.Location = new Point(46, 22);
                lblRole.AutoSize = true;

                row.Controls.Add(avatar);
                row.Controls.Add(lblName);
                row.Controls.Add(lblRole);

                panelMembers.Controls.Add(row);
            }
        }

        /// <summary>
        /// Channel хэсэгт section title нэмнэ.
        /// Жишээ нь: TEXT CHANNELS, VOICE CHANNELS
        /// </summary>
        private void AddSection(string text)
        {
            Label label = CreateLabel(text, 11, TextMuted);
            label.Dock = DockStyle.Top;
            label.Height = 28;
            label.Padding = new Padding(16, 8, 0, 0);
            label.TextAlign = ContentAlignment.TopLeft;
            panelChannels.Controls.Add(label);
        }

        /// <summary>
        /// Channel button үүсгэж panelChannels дотор нэмнэ.
        /// </summary>
        private void AddChannelButton(string text, bool active, Action onClick)
        {
            Button button = new Button();
            button.Text = text;
            button.Dock = DockStyle.Top;
            button.Height = 34;
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = active ? Color.FromArgb(57, 60, 67) : Color.Transparent;
            button.ForeColor = active ? TextLight : TextMuted;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(16, 0, 0, 0);
            button.Cursor = Cursors.Hand;

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(57, 60, 67);

            button.Click += (s, e) =>
            {
                onClick();
            };

            panelChannels.Controls.Add(button);
        }

        /// <summary>
        /// Нэг мессежийг card хэлбэрээр үүсгэж message flow panel дээр нэмнэ.
        /// Мөн 6 төрлийн reaction control нэмнэ.
        /// </summary>
        private void AddMessageCard(string username, string content)
        {
            Panel messagePanel = new Panel();
            messagePanel.Width = flpMessages.ClientSize.Width - 30;
            messagePanel.Height = 135;
            messagePanel.BackColor = Color.FromArgb(64, 68, 75);
            messagePanel.Margin = new Padding(10);

            Label lblUser = new Label();
            lblUser.Text = username;
            lblUser.ForeColor = Color.White;
            lblUser.Location = new Point(12, 10);
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            Label lblMessage = new Label();
            lblMessage.Text = content;
            lblMessage.ForeColor = TextLight;
            lblMessage.Location = new Point(12, 35);
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 11);

            // Мессеж дээр ашиглах reaction emoji-ууд
            string[] emojis = { "👍", "❤️", "😂", "😮", "😢", "👎" };

            int startX = 12;
            int y = 72;

            foreach (string emoji in emojis)
            {
                MessageReactionControl reaction = new MessageReactionControl();
                reaction.ReactionEmoji = emoji;
                reaction.ReactionCount = 0;
                reaction.IsReacted = false;
                reaction.Location = new Point(startX, y);

                // Reaction дарагдах үед event ажиллана
                reaction.ReactionToggled += Reaction_ReactionToggled;

                messagePanel.Controls.Add(reaction);

                startX += 90;
            }

            messagePanel.Controls.Add(lblUser);
            messagePanel.Controls.Add(lblMessage);

            flpMessages.Controls.Add(messagePanel);
            flpMessages.SetFlowBreak(messagePanel, true);
            flpMessages.ScrollControlIntoView(messagePanel);
        }

        /// <summary>
        /// Reaction control дарагдах үед ажиллах event handler.
        /// Одоогоор console дээр reaction төлөвийг хэвлэж байна.
        /// </summary>
        private void Reaction_ReactionToggled(object? sender, EventArgs e)
        {
            MessageReactionControl? reaction = sender as MessageReactionControl;

            if (reaction != null)
            {
                Console.WriteLine(reaction.IsReacted);
                Console.WriteLine(reaction.ReactionCount);
            }
        }

        /// <summary>
        /// Label control үүсгэх туслах method.
        /// </summary>
        private Label CreateLabel(string text, float fontSize, Color color, FontStyle style = FontStyle.Regular)
        {
            Label label = new Label();
            label.Text = text;
            label.ForeColor = color;
            label.Font = new Font("Segoe UI", fontSize, style);
            label.AutoSize = false;
            return label;
        }

        /// <summary>
        /// Дугуй button үүсгэх туслах method.
        /// Серверийн товчнууд дээр ашиглагдана.
        /// </summary>
        private static Button CreateRoundButton(string text, Color backColor)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(48, 48);
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.Cursor = Cursors.Hand;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            button.FlatAppearance.BorderSize = 0;
            button.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 48, 48, 24, 24));

            return button;
        }

        /// <summary>
        /// Username-ийн эхний үсгээс хамаарч avatar-ийн өнгө сонгоно.
        /// </summary>
        private static Color GetAvatarColor(string name)
        {
            char firstChar = 'A';

            if (name.Length > 0)
            {
                firstChar = name[0];
            }

            if (firstChar <= 'E')
            {
                return Color.FromArgb(88, 101, 242);
            }
            else if (firstChar <= 'K')
            {
                return Color.FromArgb(59, 165, 93);
            }
            else if (firstChar <= 'P')
            {
                return Color.FromArgb(244, 127, 255);
            }
            else
            {
                return Color.FromArgb(250, 166, 26);
            }
        }

        /// <summary>
        /// Windows API function.
        /// Control-ийг дугуй булантай болгоход ашиглана.
        /// </summary>
        [System.Runtime.InteropServices.DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
    }
}