using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class AppUserDocument : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
    }
}