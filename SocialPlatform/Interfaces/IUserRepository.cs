using System;
using System.Collections.Generic;

namespace SocialPlatform.Interfaces
{
    /// <summary>
    /// Хэрэглэгчийн Repository интерфэйс
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>ID-гаар хайх</summary>
        IUser? GetById(Guid id);

        /// <summary>Бүгдийг авах</summary>
        IReadOnlyList<IUser> GetAll();

        /// <summary>Нэмэх</summary>
        void Add(IUser user);

        /// <summary>Шинэчлэх</summary>
        void Update(IUser user);

        /// <summary>Устгах</summary>
        void Remove(IUser user);

        /// <summary>И-мэйлээр хайх</summary>
        IUser? GetByEmail(string email);

        /// <summary>Хэрэглэгчийн нэрээр хайх</summary>
        IUser? GetByUsername(string username);

    }
}