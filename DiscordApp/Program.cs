using DiscordApp.Cores;
using DiscordApp.Enums;
using SocialNetworkingPlatform.Chats;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;
using SocialNetworkingPlatform.Repositories;
using SocialNetworkingPlatform.Services;
using SocialNetworkingPlatform.Enums;
using SocialPlatform.Interfaces;

namespace DiscordApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepo = new UserRepository();
            IPostRepository postRepo = new PostRepository();
            IChatRepository chatRepo = new ChatRepository();

            var userService = new UserService(userRepo);
            var postService = new PostService(postRepo);
            var chatService = new ChatService(chatRepo);

            var platform = new DiscordPlatform("Discord", userRepo, postRepo);
            Console.WriteLine($"{platform}");
            Console.WriteLine("\nSign up users");

            var user1 = (DiscordUser)platform.SignUp("Tuguldur", "tguldurr", "tuguldur@gmail.com", "pass123", new DateTime(2000, 1, 1));
            var user2 = (DiscordUser)platform.SignUp("Bold", "bold456", "bold@gmail.com", "pass456", new DateTime(1999, 5, 15));
            var user3 = (DiscordUser)platform.SignUp("Anu", "anuuu", "anu@gmail.com", "pass789", new DateTime(2001, 3, 20));
            var user4 = (DiscordUser)platform.SignUp("Sara", "sara789", "sara@gmail.com", "pass321", new DateTime(1998, 7, 30));
            var user5 = (DiscordUser)platform.SignUp("Enkhtuya", "enkhtuya123", "enkhtuya@gmail.com", "pass654", new DateTime(2002, 11, 10));


            Console.WriteLine($"{user1}");
            Console.WriteLine($"{user2}");
            Console.WriteLine($"{user3}");
            Console.WriteLine($"{user4}");
            Console.WriteLine($"{user5}");

            Console.WriteLine("\nLogin");

            var loggedIn = userService.Login("tuguldur@gmail.com", "pass123");
            Console.WriteLine(loggedIn != null ? $"Successul: {loggedIn}" : "Unsuccessful");

            Console.WriteLine("\nAdd a friend");

            user1.AddFriend(user2);
            user1.AddFriend(user3);
            user1.AddFriend(user4);
            user2.AddFriend(user5);
            Console.WriteLine($"{user1.Username} friend: {user1.FriendCount}");
            Console.WriteLine($"{user2.Username} friend: {user2.FriendCount}");
            Console.WriteLine($"{user3.Username} friend: {user3.FriendCount}");

            Console.WriteLine("\nRemove a friend");

            user1.RemoveFriend(user3);
            Console.WriteLine($"{user1.Username} friend: {user1.FriendCount}");
            Console.WriteLine($"{user2.Username} friend: {user2.FriendCount}");
            Console.WriteLine($"{user3.Username} friend: {user3.FriendCount}");


            Console.WriteLine("\nServer");

            var server = new Server("Mongolian Devs", user1);
            server.AddMember(user2, RoleType.Member);
            var textChannel = server.AddTextChannel("general");
            var voiceChannel = server.AddVoiceChannel("voice");

            Console.WriteLine($"{server}");
            Console.WriteLine($"{user1.Username}: {server.GetRole(user1.Id)}");
            Console.WriteLine($"{user2.Username}: {server.GetRole(user2.Id)}");
            Console.WriteLine($"{user1}");
            Console.WriteLine($"{user2}");

            Console.WriteLine("\nRemove a member");
            server.RemoveMember(user2);
            Console.WriteLine($"{server}");


            Console.WriteLine("\nMessage");

            var msg1 = textChannel.SendMessage(user1.Id, "Hi");
            var msg2 = textChannel.SendMessage(user2.Id, "Hi, How are you");


            var attachment1 = new Attachment("image.png", "http://example.com/image.png", AttachmentType.Image, 15);
            var msg3 = textChannel.SendMessage(user1.Id, "I am ", attachment1);
            Console.WriteLine($"{user1.Username}: {msg1.Content}");
            Console.WriteLine($"{user2.Username}: {msg2.Content}");
            Console.WriteLine($"Messages in {textChannel.Name}: {textChannel.Messages.Count}");
            Console.WriteLine($"{textChannel.GetMessages()}");


            Console.WriteLine("\nReaction");

            msg1.AddReaction(user2.Id, ReactionType.Like);
            msg1.AddReaction(user1.Id, ReactionType.Love);
            Console.WriteLine($"Reactions: {msg1.Reactions.Count}");


            Console.WriteLine("\nChange a role");

            server.ChangeMemberRole(user2.Id, RoleType.Moderator);
            Console.WriteLine($"{user2.Username} role: {server.GetRole(user2.Id)}");

            Console.WriteLine("\nVoice Channel");

            voiceChannel.Join(user1.Id);
            voiceChannel.Join(user2.Id);
            Console.WriteLine($"{voiceChannel.Name}: {voiceChannel.ActiveUserCount} users");


            Console.WriteLine("\nDM");

            var dm = chatService.CreateSingleChat(user1.Id, user2.Id);
            chatService.SendMessage(dm.Id, user1.Id, "Hi Bob!");
            chatService.SendMessage(dm.Id, user2.Id, "Bye Alice!");

            foreach (var msg in dm.Messages)
            {
                var sender = userRepo.GetById(msg.SenderId);
                Console.WriteLine($"{sender?.Username}: {msg.Content}");
            }

            Console.WriteLine("\nGroup Chat");

            var group = (GroupChat)chatService.CreateGroupChat(user1.Id, "Dev Friends");
            group.AddMember(user2.Id);
            chatService.SendMessage(group.Id, user1.Id, "Hello Everybody!");
            Console.WriteLine($"{group}");


            Console.WriteLine("\n══════════════════════════");
            Console.WriteLine($"Users: {userRepo.GetAll().Count}");
            Console.WriteLine($"Serversr: {server.Name} |{server.Members.Count}");
            Console.WriteLine($"DM: {dm.Messages.Count}");
            Console.WriteLine($"Voice: {voiceChannel.ActiveUserCount} users");
            Console.WriteLine("══════════════════════════");
        }
    }
}