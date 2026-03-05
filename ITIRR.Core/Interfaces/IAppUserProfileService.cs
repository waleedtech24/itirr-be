using ITIRR.Core.DTOs.User;

namespace ITIRR.Core.Interfaces
{
    public interface IAppUserProfileService
    {
        Task<AppUserProfileResponse?> GetProfileAsync(string userId);
        Task<AppUserProfileResponse> UpdateBasicInfoAsync(string userId, AppUserUpdateBasicRequest request);
        Task<AppUserProfileResponse> UpdateContactAsync(string userId, AppUserUpdateContactRequest request);
        Task<AppUserProfileResponse> UploadPhotoAsync(string userId, Stream photoStream, string fileName);
        Task<AppUserDocumentResponse> UploadDocumentAsync(string userId, Stream fileStream, string fileName, long fileSize);
        Task<List<AppUserDocumentResponse>> GetDocumentsAsync(string userId);
        Task<bool> DeleteDocumentAsync(string userId, Guid documentId);
    }
}