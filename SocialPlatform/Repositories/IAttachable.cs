using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetworkingPlatform.Interfaces
{
    public interface IAttachable
    {
        void AttachFile(IAttachment attachment);
        void DetachFile(Guid attachmentId);
    }
}
