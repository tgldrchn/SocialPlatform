using System;
using System.Collections.Generic;

namespace SocialNetworkingPlatform.Interfaces
{
    /// <summary>
    /// Чатын Repository интерфэйс
    /// </summary>
    public interface IChatRepository
    {
        IChat? GetById(Guid id);
        IReadOnlyList<IChat> GetAll();
        void Add(IChat chat);
        void Remove(IChat chat);
        IEnumerable<IChat> GetByUser(Guid userId);
        IChat? GetSingleChat(Guid user1Id, Guid user2Id);
    }
}
