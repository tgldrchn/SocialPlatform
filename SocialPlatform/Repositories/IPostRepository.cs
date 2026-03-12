using System;
using System.Collections.Generic;

namespace SocialNetworkingPlatform.Interfaces
{
    /// <summary>
    /// Нийтлэлийн Repository интерфэйс
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>ID-гаар хайх</summary>
        IPost? GetById(Guid id);

        /// <summary>Бүгдийг авах</summary>
        IReadOnlyList<IPost> GetAll();

        /// <summary>Нэмэх</summary>
        void Add(IPost post);

        /// <summary>Шинэчлэх</summary>
        void Update(IPost post);

        /// <summary>Устгах</summary>
        void Remove(IPost post);

        /// <summary>Зохиогчоор хайх</summary>
        IEnumerable<IPost> GetByAuthor(Guid authorId);

        /// <summary>Feed авах</summary>
        IEnumerable<IPost> GetFeed(Guid userId);
    }
}