using DiscordApp.Winforms.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DiscordApp.Winforms
{
    /// <summary>
    /// Message дээр ашиглагдах reaction control.
    /// Emoji + count харуулж, дарахад reaction нэмэх/хасах боломжтой.
    /// </summary>
    public partial class MessageReactionControl : UserControl
    {
        // Reaction мэдээлэл хадгалах хувьсагчууд
        private string _reactionEmoji = "👍";
        private int _reactionCount = 0;
        private bool _isReacted = false;

        // Өнгөний тохиргоо
        private Color _normalReactionBackColor = Color.FromArgb(60, 63, 65);
        private Color _reactedReactionBackColor = Color.FromArgb(88, 101, 242);
        private Color _reactionTextColor = Color.White;

        /// <summary>
        /// Constructor - control-ийн анхны тохиргоо
        /// </summary>
        public MessageReactionControl()
        {
            DoubleBuffered = true; // flicker багасгана
            Size = new Size(70, 30);
            Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Reaction emoji (жишээ: 👍 ❤️ 😂)
        /// </summary>
        [Category("Reaction")]
        [Description("Reaction emoji text.")]
        [DefaultValue("👍")]
        public string ReactionEmoji
        {
            get { return _reactionEmoji; }
            set
            {
                _reactionEmoji = value;
                Invalidate(); // redraw
            }
        }

        /// <summary>
        /// Reaction-ийн тоо
        /// </summary>
        [Category("Reaction")]
        [Description("Reaction count.")]
        [DefaultValue(0)]
        public int ReactionCount
        {
            get { return _reactionCount; }
            set
            {
                _reactionCount = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Хэрэглэгч reaction хийсэн эсэх
        /// </summary>
        [Category("Reaction")]
        [Description("Whether current user reacted or not.")]
        [DefaultValue(false)]
        public bool IsReacted
        {
            get { return _isReacted; }
            set
            {
                _isReacted = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Reaction хийгдээгүй үед background өнгө
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "60, 63, 65")]
        public Color NormalReactionBackColor
        {
            get { return _normalReactionBackColor; }
            set
            {
                _normalReactionBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Reaction хийгдсэн үед background өнгө
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "60, 63, 65")]
        public Color ReactedReactionBackColor
        {
            get { return _reactedReactionBackColor; }
            set
            {
                _reactedReactionBackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Emoji болон count-ийн текстийн өнгө
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "60, 63, 65")]
        public Color ReactionTextColor
        {
            get { return _reactionTextColor; }
            set
            {
                _reactionTextColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Control-г зурах (Draw хийх хэсэг)
        /// Graphics ашиглан custom UI үүсгэнэ.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Background өнгө сонгох
            Color backColor = IsReacted ? ReactedReactionBackColor : NormalReactionBackColor;

            using (SolidBrush bgBrush = new SolidBrush(backColor))
            using (SolidBrush textBrush = new SolidBrush(ReactionTextColor))
            using (Pen borderPen = new Pen(Color.Gray))
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

                // Дугуй булантай rectangle зурна
                e.Graphics.FillRoundedRectangle(bgBrush, rect, 15);
                e.Graphics.DrawRoundedRectangle(borderPen, rect, 15);

                // Emoji болон count бичих
                using (Font emojiFont = new Font("Segoe UI Emoji", 11))
                using (Font countFont = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    e.Graphics.DrawString(ReactionEmoji, emojiFont, textBrush, 10, 6);
                    e.Graphics.DrawString(ReactionCount.ToString(), countFont, textBrush, 38, 7);
                }
            }
        }

        /// <summary>
        /// Control дээр дарахад ажиллах event.
        /// Reaction toggle хийж count-ийг нэмэх/хасна.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            // Toggle хийх
            IsReacted = !IsReacted;

            // Count update хийх
            if (IsReacted)
                ReactionCount++;
            else
                ReactionCount--;

            // Custom event дуудах
            ReactionToggled?.Invoke(this,
                new ReactionToggledEventArgs(ReactionEmoji, ReactionCount, IsReacted));
        }

        /// <summary>
        /// Reaction дарагдсан үед үүсэх custom event
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the reaction is clicked.")]
        public event EventHandler<ReactionToggledEventArgs>? ReactionToggled;
    }

    /// <summary>
    /// Graphics-д зориулсан helper extension method-ууд.
    /// Дугуй булантай rectangle зурахад ашиглагдана.
    /// </summary>
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle bounds, int radius)
        {
            using (var path = CreateRoundedRectanglePath(bounds, radius))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle bounds, int radius)
        {
            using (var path = CreateRoundedRectanglePath(bounds, radius))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// Дугуй булантай rectangle path үүсгэнэ
        /// </summary>
        private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}