using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Repositories;
using System;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Бүх платформын суурь abstract класс
    /// </summary>
    public abstract class Platform
    {
        // ✅ Interface төрөл
        protected readonly IUserRepository UserRepository;
        protected readonly IPostRepository PostRepository;

        /// <summary>Платформын нэр</summary>
        public string Name { get; private set; }

        /// <summary>Платформын хувилбар</summary>
        public string Version { get; set; } = "1.0";

        /// <summary>Бүртгэлтэй хэрэглэгчид</summary>
        public IReadOnlyList<IUser> Users => UserRepository.GetAll();

        /// <summary>Нийтлэлүүд</summary>
        public IReadOnlyList<IPost> Posts => PostRepository.GetAll();

        // ✅ Interface төрлөөр авна
        protected Platform(string name,
                           IUserRepository userRepository,
                           IPostRepository postRepository)
        {
            Name = name;
            UserRepository = userRepository;
            PostRepository = postRepository;
        }

        /// <summary>Шинэ хэрэглэгч бүртгэх</summary>
        public virtual IUser SignUp(string name, string username,
                                    string email, string password,
                                    DateTime dateOfBirth)
        {
            if (UserRepository.GetByEmail(email) != null)
                throw new Exception($"{email} аль хэдийн бүртгэлтэй.");

            var user = new User(name, username, email, password, dateOfBirth);
            UserRepository.Add(user);
            return user;
        }

        /// <summary>Хэрэглэгч устгах</summary>
        public void DeleteUser(Guid id)
        {
            var user = UserRepository.GetById(id);
            if (user != null)
                UserRepository.Remove(user);
        }

        /// <summary>Пост үүсгэх — платформ бүр өөрөөр хэрэгжүүлнэ</summary>
        public abstract IPost CreatePost(Guid authorId, string content);

        /// <summary>Пост устгах</summary>
        public void DeletePost(Guid id)
        {
            var post = PostRepository.GetById(id);
            if (post != null)
                PostRepository.Remove(post);
        }

        public override string ToString() =>
            $"[{Name} v{Version}] User Count: {Users.Count} Post Count:{Posts.Count}";
    }
}
