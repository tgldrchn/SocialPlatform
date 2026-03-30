using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SocialNetworkingPlatform.Interfaces
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Username { get; }
       
        bool VerifyPassword(string password);

    }
}
