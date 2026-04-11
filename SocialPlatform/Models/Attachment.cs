using SocialNetworkingPlatform.Enums;
using SocialPlatform.Interfaces;
using System;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Мессеж, постын хавсралт
    /// </summary>
    public class Attachment : IAttachment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FileName { get; private set; }
        public string Url { get; private set; }
        public AttachmentType FileType { get; private set; }
        public long FileSize { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Attachment(string fileName, string url,
                          AttachmentType fileType, long fileSize)
        {
            FileName = fileName;
            Url = url;
            FileType = fileType;
            FileSize = fileSize;
        }

        public override string ToString() =>
            $"[{FileType}] {FileName} ({FileSize} bytes)";
    }
}
