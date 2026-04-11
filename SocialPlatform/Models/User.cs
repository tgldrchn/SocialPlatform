using System;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Хэрэглэгчийн класс
    /// </summary>
    public class User : IUser
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public byte Age { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User(string name, string username, string email,
                    string password, DateTime dateOfBirth)
        {
            Name = name;
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
            Age = CalculateAge(dateOfBirth);
            PasswordSalt = GenerateSalt();
            PasswordHash = HashPassword(password, PasswordSalt);
        }

        private static byte CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            return (byte)age;
        }

        private static string GenerateSalt() =>
            Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        private static string HashPassword(string password, string salt) =>
            Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(password + salt));

        public bool VerifyPassword(string password) =>
            HashPassword(password, PasswordSalt) == PasswordHash;

        public override string ToString() =>
            $"[User] {Username} ({Name}) - {Email}";
    }
}