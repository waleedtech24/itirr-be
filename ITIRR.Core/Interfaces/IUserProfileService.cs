using ITIRR.Core.DTOs.Profile;

namespace ITIRR.Core.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse?> GetProfileAsync(string userId);
        Task<UserProfileResponse> UpdateBasicInfoAsync(UpdateBasicInfoRequest request, string userId);
        Task<UserProfileResponse> UpdateContactAsync(UpdateContactRequest request, string userId);
        Task<UserProfileResponse> UpdateProfilePhotoAsync(Stream photo, string fileName, string userId);
        Task<IEnumerable<UserDocumentResponse>> GetDocumentsAsync(string userId);
        Task<UserDocumentResponse> UploadDocumentAsync(Stream file, string fileName, long fileSize, string userId);
        Task DeleteDocumentAsync(Guid documentId, string userId);
    }
}