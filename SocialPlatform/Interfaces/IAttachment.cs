using SocialNetworkingPlatform.Enums;

namespace SocialPlatform.Interfaces
{
    public interface IAttachment
    {
        Guid Id { get; }
        string FileName { get; }
        string Url { get; }
        AttachmentType FileType { get; }
        long FileSize { get; }
        DateTime CreatedAt { get; }
    }

}
