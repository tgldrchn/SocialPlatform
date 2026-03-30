namespace DiscordApp.Winforms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null!;

        private Panel panelLogin;
        private Panel panelRegister;
        private Panel panelMain;

        private Label lblLoginTitle;
        private TextBox txtLoginEmail;
        private TextBox txtLoginPassword;
        private Button btnLogin;
        private LinkLabel lnkGoRegister;

        private Label lblRegisterTitle;
        private TextBox txtRegisterName;
        private TextBox txtRegisterUsername;
        private TextBox txtRegisterEmail;
        private TextBox txtRegisterPassword;
        private Button btnRegister;
        private LinkLabel lnkBackToLogin;

        private Panel panelServers;
        private Panel panelChannels;
        private Panel panelChat;
        private Panel panelChatHeader;
        private FlowLayoutPanel flpMessages;
        private Panel panelChatInput;
        private TextBox txtMessageInput;
        private Button btnSend;
        private Panel panelMembers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLogin = new Panel();
            lblLoginTitle = new Label();
            txtLoginEmail = new TextBox();
            txtLoginPassword = new TextBox();
            btnLogin = new Button();
            lnkGoRegister = new LinkLabel();
            panelRegister = new Panel();
            lblRegisterTitle = new Label();
            txtRegisterName = new TextBox();
            txtRegisterUsername = new TextBox();
            txtRegisterEmail = new TextBox();
            txtRegisterPassword = new TextBox();
            btnRegister = new Button();
            lnkBackToLogin = new LinkLabel();
            panelMain = new Panel();
            panelChat = new Panel();
            flpMessages = new FlowLayoutPanel();
            panelChatInput = new Panel();
            txtMessageInput = new TextBox();
            btnSend = new Button();
            panelChatHeader = new Panel();
            panelMembers = new Panel();
            panelChannels = new Panel();
            panelServers = new Panel();
            panelLogin.SuspendLayout();
            panelRegister.SuspendLayout();
            panelMain.SuspendLayout();
            panelChat.SuspendLayout();
            panelChatInput.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(lblLoginTitle);
            panelLogin.Controls.Add(txtLoginEmail);
            panelLogin.Controls.Add(txtLoginPassword);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(lnkGoRegister);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(0, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(1200, 700);
            panelLogin.TabIndex = 0;
            // 
            // lblLoginTitle
            // 
            lblLoginTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblLoginTitle.Location = new Point(0, 70);
            lblLoginTitle.Name = "lblLoginTitle";
            lblLoginTitle.Size = new Size(1200, 44);
            lblLoginTitle.TabIndex = 0;
            lblLoginTitle.Text = "Discord";
            lblLoginTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLoginEmail
            // 
            txtLoginEmail.Location = new Point(500, 180);
            txtLoginEmail.Name = "txtLoginEmail";
            txtLoginEmail.PlaceholderText = "И-мэйл";
            txtLoginEmail.Size = new Size(220, 27);
            txtLoginEmail.TabIndex = 1;
            // 
            // txtLoginPassword
            // 
            txtLoginPassword.Location = new Point(500, 225);
            txtLoginPassword.Name = "txtLoginPassword";
            txtLoginPassword.PasswordChar = '•';
            txtLoginPassword.PlaceholderText = "Нууц үг";
            txtLoginPassword.Size = new Size(220, 27);
            txtLoginPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(500, 270);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(220, 40);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Нэвтрэх";
            // 
            // lnkGoRegister
            // 
            lnkGoRegister.Location = new Point(500, 320);
            lnkGoRegister.Name = "lnkGoRegister";
            lnkGoRegister.Size = new Size(220, 24);
            lnkGoRegister.TabIndex = 4;
            lnkGoRegister.TabStop = true;
            lnkGoRegister.Text = "Бүртгэлгүй юу? Бүртгүүлэх →";
            // 
            // panelRegister
            // 
            panelRegister.Controls.Add(lblRegisterTitle);
            panelRegister.Controls.Add(txtRegisterName);
            panelRegister.Controls.Add(txtRegisterUsername);
            panelRegister.Controls.Add(txtRegisterEmail);
            panelRegister.Controls.Add(txtRegisterPassword);
            panelRegister.Controls.Add(btnRegister);
            panelRegister.Controls.Add(lnkBackToLogin);
            panelRegister.Dock = DockStyle.Fill;
            panelRegister.Location = new Point(0, 0);
            panelRegister.Name = "panelRegister";
            panelRegister.Size = new Size(1200, 700);
            panelRegister.TabIndex = 1;
            // 
            // lblRegisterTitle
            // 
            lblRegisterTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblRegisterTitle.Location = new Point(0, 60);
            lblRegisterTitle.Name = "lblRegisterTitle";
            lblRegisterTitle.Size = new Size(1200, 36);
            lblRegisterTitle.TabIndex = 0;
            lblRegisterTitle.Text = "Бүртгүүлэх";
            lblRegisterTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtRegisterName
            // 
            txtRegisterName.Location = new Point(500, 140);
            txtRegisterName.Name = "txtRegisterName";
            txtRegisterName.PlaceholderText = "Нэр";
            txtRegisterName.Size = new Size(220, 27);
            txtRegisterName.TabIndex = 1;
            // 
            // txtRegisterUsername
            // 
            txtRegisterUsername.Location = new Point(500, 184);
            txtRegisterUsername.Name = "txtRegisterUsername";
            txtRegisterUsername.PlaceholderText = "Хэрэглэгчийн нэр";
            txtRegisterUsername.Size = new Size(220, 27);
            txtRegisterUsername.TabIndex = 2;
            // 
            // txtRegisterEmail
            // 
            txtRegisterEmail.Location = new Point(500, 228);
            txtRegisterEmail.Name = "txtRegisterEmail";
            txtRegisterEmail.PlaceholderText = "И-мэйл";
            txtRegisterEmail.Size = new Size(220, 27);
            txtRegisterEmail.TabIndex = 3;
            // 
            // txtRegisterPassword
            // 
            txtRegisterPassword.Location = new Point(500, 272);
            txtRegisterPassword.Name = "txtRegisterPassword";
            txtRegisterPassword.PasswordChar = '•';
            txtRegisterPassword.PlaceholderText = "Нууц үг";
            txtRegisterPassword.Size = new Size(220, 27);
            txtRegisterPassword.TabIndex = 4;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(500, 320);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(220, 40);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Бүртгүүлэх";
            // 
            // lnkBackToLogin
            // 
            lnkBackToLogin.Location = new Point(500, 370);
            lnkBackToLogin.Name = "lnkBackToLogin";
            lnkBackToLogin.Size = new Size(220, 24);
            lnkBackToLogin.TabIndex = 6;
            lnkBackToLogin.TabStop = true;
            lnkBackToLogin.Text = "← Буцах";
            // 
            // panelMain
            // 
            panelMain.Controls.Add(panelChat);
            panelMain.Controls.Add(panelMembers);
            panelMain.Controls.Add(panelChannels);
            panelMain.Controls.Add(panelServers);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1200, 700);
            panelMain.TabIndex = 2;
            // 
            // panelChat
            // 
            panelChat.Controls.Add(flpMessages);
            panelChat.Controls.Add(panelChatInput);
            panelChat.Controls.Add(panelChatHeader);
            panelChat.Dock = DockStyle.Fill;
            panelChat.Location = new Point(292, 0);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(688, 700);
            panelChat.TabIndex = 0;
            // 
            // flpMessages
            // 
            flpMessages.AutoScroll = true;
            flpMessages.Dock = DockStyle.Fill;
            flpMessages.FlowDirection = FlowDirection.TopDown;
            flpMessages.Location = new Point(0, 48);
            flpMessages.Name = "flpMessages";
            flpMessages.Size = new Size(688, 588);
            flpMessages.TabIndex = 0;
            flpMessages.WrapContents = false;
            // 
            // panelChatInput
            // 
            panelChatInput.Controls.Add(txtMessageInput);
            panelChatInput.Controls.Add(btnSend);
            panelChatInput.Dock = DockStyle.Bottom;
            panelChatInput.Location = new Point(0, 636);
            panelChatInput.Name = "panelChatInput";
            panelChatInput.Padding = new Padding(16, 12, 16, 12);
            panelChatInput.Size = new Size(688, 64);
            panelChatInput.TabIndex = 1;
            // 
            // txtMessageInput
            // 
            txtMessageInput.BorderStyle = BorderStyle.None;
            txtMessageInput.Dock = DockStyle.Fill;
            txtMessageInput.Font = new Font("Segoe UI", 12F);
            txtMessageInput.Location = new Point(16, 12);
            txtMessageInput.Name = "txtMessageInput";
            txtMessageInput.PlaceholderText = "Бичих...";
            txtMessageInput.Size = new Size(608, 27);
            txtMessageInput.TabIndex = 0;
            // 
            // btnSend
            // 
            btnSend.Dock = DockStyle.Right;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Location = new Point(624, 12);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(48, 40);
            btnSend.TabIndex = 1;
            btnSend.Text = "➤";
            // 
            // panelChatHeader
            // 
            panelChatHeader.Dock = DockStyle.Top;
            panelChatHeader.Location = new Point(0, 0);
            panelChatHeader.Name = "panelChatHeader";
            panelChatHeader.Size = new Size(688, 48);
            panelChatHeader.TabIndex = 2;
            // 
            // panelMembers
            // 
            panelMembers.Dock = DockStyle.Right;
            panelMembers.Location = new Point(980, 0);
            panelMembers.Name = "panelMembers";
            panelMembers.Size = new Size(220, 700);
            panelMembers.TabIndex = 1;
            // 
            // panelChannels
            // 
            panelChannels.Dock = DockStyle.Left;
            panelChannels.Location = new Point(72, 0);
            panelChannels.Name = "panelChannels";
            panelChannels.Size = new Size(220, 700);
            panelChannels.TabIndex = 2;
            // 
            // panelServers
            // 
            panelServers.Dock = DockStyle.Left;
            panelServers.Location = new Point(0, 0);
            panelServers.Name = "panelServers";
            panelServers.Size = new Size(72, 700);
            panelServers.TabIndex = 3;
            // 
            // MainForm
            // 
            ClientSize = new Size(1200, 700);
            Controls.Add(panelLogin);
            Controls.Add(panelRegister);
            Controls.Add(panelMain);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Discord";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelRegister.ResumeLayout(false);
            panelRegister.PerformLayout();
            panelMain.ResumeLayout(false);
            panelChat.ResumeLayout(false);
            panelChatInput.ResumeLayout(false);
            panelChatInput.PerformLayout();
            ResumeLayout(false);
        }
    }
}