namespace DiscordApp.Winforms.Controls
{
    /// <summary>
    /// Reaction дарсан үед дамжуулах мэдээллийг агуулсан класс.
    /// Custom event ашиглах үед энэ класс хэрэглэгдэнэ.
    /// </summary>
    public class ReactionToggledEventArgs : EventArgs
    {
        /// <summary>
        /// Дарсан emoji (жишээ: 👍 ❤️ 😂)
        /// </summary>
        public string Emoji { get; }

        /// <summary>
        /// Reaction-ийн тоо (count)
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Хэрэглэгч reaction хийсэн эсэх (true / false)
        /// </summary>
        public bool IsReacted { get; }

        /// <summary>
        /// Constructor - Reaction-ийн мэдээллийг онооно.
        /// </summary>
        /// <param name="emoji">Emoji тэмдэг</param>
        /// <param name="count">Reaction-ийн тоо</param>
        /// <param name="isReacted">Хэрэглэгч reaction хийсэн эсэх</param>
        public ReactionToggledEventArgs(string emoji, int count, bool isReacted)
        {
            Emoji = emoji;
            Count = count;
            IsReacted = isReacted;
        }
    }
}