using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkingPlatform.Interfaces;

namespace SocialNetworkingPlatform.Repositories
{
    /// <summary>
    /// Постын санах ойн Repository хэрэгжүүлэлт
    /// </summary>
    public class PostRepository : IPostRepository
    {
        public PostRepository() { }
        private readonly List<IPost> _posts = new();

        /// <summary>ID-гаар хайх</summary>
        public IPost? GetById(Guid id) =>
            _posts.FirstOrDefault(p => p.Id == id);

        /// <summary>Бүгдийг авах</summary>
        public IReadOnlyList<IPost> GetAll() =>
            _posts.AsReadOnly();

        /// <summary>Нэмэх</summary>
        public void Add(IPost post) =>
            _posts.Add(post);

        /// <summary>Шинэчлэх</summary>
        public void Update(IPost post)
        {
            var idx = _posts.FindIndex(p => p.Id == post.Id);
            if (idx != -1)
                _posts[idx] = post;
        }

        /// <summary>Устгах</summary>
        public void Remove(IPost post) =>
            _posts.Remove(post);

        /// <summary>Зохиогчоор хайх</summary>
        public IEnumerable<IPost> GetByAuthor(Guid authorId) =>
            _posts.Where(p => p.AuthorId == authorId);

        /// <summary>Feed авах</summary>
        public IEnumerable<IPost> GetFeed(Guid userId) =>
            _posts.OrderByDescending(p => p.CreatedAt);
    }
}
