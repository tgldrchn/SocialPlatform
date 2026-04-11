using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Interfaces
{
    public interface IAttachable
    {
        void AttachFile(IAttachment attachment);
        void DetachFile(Guid attachmentId);
    }
}
