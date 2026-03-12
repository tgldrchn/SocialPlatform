using System;
using System.Collections.Generic;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;

namespace DiscordApp.Channels
{
    /// <summary>
    /// Текст суваг — мессеж илгээх боломжтой
    /// </summary>
    public class TextChannel : Channel, IMessageable
    {
        private readonly List<IMessage> _messages = new();
        private readonly List<IPost> _posts = new();
        public IReadOnlyList<IMessage> Messages => _messages;
        public IReadOnlyList<IPost> Posts => _posts;

        public TextChannel(string name)
            : base(name) { }


        /// <summary>Мессеж илгээх</summary>
        public IMessage SendMessage(Guid senderId, string content, IAttachment? attachment = null)
        {
            var message = new Message(senderId, Id, content, attachment);
            _messages.Add(message);
            return message;
        }

        // Post нийтлэх — урт агуулга
        public IPost Publish(Guid authorId, string content)
        {
            var post = new Post(authorId, content);
            _posts.Add(post);
            return post;
        }

        public void DeleteMessage(Guid messageId)
        {
            var message = _messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
                _messages.Remove(message);
        }

        public void DeletePost(Guid postId)
        {
            var post = _posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
                _posts.Remove(post);
        }

        public string GetChannelInfo()
        {
            return $"Text Channel: {Name}, Messages: {_messages.Count}, Posts: {_posts.Count}";
        }

        public string GetMessages()
        {
            return string.Join("\n", _messages.Select(m => $"[{m.CreatedAt}] User {m.SenderId}: {m.Content}"));
        }
    }
}