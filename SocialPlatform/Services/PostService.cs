using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;

namespace SocialNetworkingPlatform.Services
{
    /// <summary>
    /// Нийтлэлийн бизнес логик
    /// </summary>
    public class PostService
    {
        private readonly IPostRepository _postRepo;
        public PostService(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        /// <summary>Пост үүсгэх</summary>
        public IPost CreatePost(Guid authorId, string content)
        {
            var post = new Post(authorId, content);
            _postRepo.Add(post);
            return post;
        }

        /// <summary>Хэрэглэгчийн постуудыг авах</summary>
        public IEnumerable<IPost> GetByAuthor(Guid authorId) =>
            _postRepo.GetByAuthor(authorId);

        /// <summary>Feed авах</summary>
        public IEnumerable<IPost> GetFeed(Guid userId) =>
            _postRepo.GetFeed(userId);
    }
}
