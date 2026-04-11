using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // Бүртгэх
        public IUser Register(string name, string username,
                              string email, string password,
                              DateTime dateOfBirth)
        {
            // И-мэйл давхардсан эсэх шалгах
            if (_userRepo.GetByEmail(email) != null)
                throw new Exception($"{email} аль хэдийн бүртгэлтэй.");

            var user = new User(name, username, email, password, dateOfBirth);
            _userRepo.Add(user);
            return user;
        }

        // Нэвтрэх
        public IUser? Login(string email, string password)
        {
            var user = _userRepo.GetByEmail(email);
            if (user == null) return null;
            // Нууц үг шалгах

            if (user.VerifyPassword(password))
                return user;
            return null;
        }
    }
}
