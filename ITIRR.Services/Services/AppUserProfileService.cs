using ITIRR.Core.DTOs.User;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ITIRR.Services.Services
{
    public class AppUserProfileService : IAppUserProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AppUserProfileService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<AppUserProfileResponse?> GetProfileAsync(string userId)
        {
            var guid = Guid.Parse(userId);
            var user = await _context.CustomerUsers
                .FirstOrDefaultAsync(u => u.Id == guid);
            return user == null ? null : MapToResponse(user);
        }

        public async Task<AppUserProfileResponse> UpdateBasicInfoAsync(
            string userId, AppUserUpdateBasicRequest request)
        {
            var user = await GetUserAsync(userId);
            user.AgencyName = request.AgencyName;
            user.About = request.About;
            user.Languages = request.Languages;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(user);
        }

        public async Task<AppUserProfileResponse> UpdateContactAsync(
            string userId, AppUserUpdateContactRequest request)
        {
            var user = await GetUserAsync(userId);
            user.ContactPhone = request.PhoneNumber;
            user.ContactEmail = request.Email;
            user.Facebook = request.Facebook;
            user.Twitter = request.Twitter;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(user);
        }

        public async Task<AppUserProfileResponse> UploadPhotoAsync(
            string userId, Stream photoStream, string fileName)
        {
            var user = await GetUserAsync(userId);
            var folder = Path.Combine(_env.WebRootPath, "uploads", "user-photos");
            Directory.CreateDirectory(folder);

            if (!string.IsNullOrEmpty(user.ProfilePhotoUrl))
            {
                var oldPath = Path.Combine(_env.WebRootPath,
                    user.ProfilePhotoUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }

            var ext = Path.GetExtension(fileName);
            var newFile = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, newFile);

            using (var fs = new FileStream(filePath, FileMode.Create))
                await photoStream.CopyToAsync(fs);

            user.ProfilePhotoUrl = $"uploads/user-photos/{newFile}";
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(user);
        }

        public async Task<AppUserDocumentResponse> UploadDocumentAsync(
            string userId, Stream fileStream, string fileName, long fileSize)
        {
            var user = await GetUserAsync(userId);
            var folder = Path.Combine(_env.WebRootPath, "uploads", "user-documents");
            Directory.CreateDirectory(folder);

            var ext = Path.GetExtension(fileName).ToLower();
            var fileType = ext == ".pdf" ? "pdf" : "image";
            var newFile = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, newFile);

            using (var fs = new FileStream(filePath, FileMode.Create))
                await fileStream.CopyToAsync(fs);

            var doc = new AppUserDocument
            {
                UserId = user.Id,
                FileName = fileName,
                FileUrl = $"uploads/user-documents/{newFile}",
                FileType = fileType,
                FileSize = fileSize
            };

            _context.CustomerDocuments.Add(doc);
            await _context.SaveChangesAsync();
            return MapDocToResponse(doc);
        }

        public async Task<List<AppUserDocumentResponse>> GetDocumentsAsync(string userId)
        {
            var guid = Guid.Parse(userId);
            var docs = await _context.CustomerDocuments
                .Where(d => d.UserId == guid)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
            return docs.Select(MapDocToResponse).ToList();
        }

        public async Task<bool> DeleteDocumentAsync(string userId, Guid documentId)
        {
            var guid = Guid.Parse(userId);
            var doc = await _context.CustomerDocuments
                .FirstOrDefaultAsync(d => d.Id == documentId && d.UserId == guid);

            if (doc == null) return false;

            var filePath = Path.Combine(_env.WebRootPath,
                doc.FileUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(filePath)) File.Delete(filePath);

            _context.CustomerDocuments.Remove(doc);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<User> GetUserAsync(string userId)
        {
            var guid = Guid.Parse(userId);
            var user = await _context.CustomerUsers
                .FirstOrDefaultAsync(u => u.Id == guid);
            if (user == null) throw new Exception("User not found.");
            return user;
        }

        private static AppUserProfileResponse MapToResponse(User u) => new()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            IsVerified = u.IsVerified,
            Role = u.Role,
            AgencyName = u.AgencyName,
            About = u.About,
            Languages = u.Languages,
            ProfilePhotoUrl = u.ProfilePhotoUrl,
            ContactPhone = u.ContactPhone,
            ContactEmail = u.ContactEmail,
            Facebook = u.Facebook,
            Twitter = u.Twitter
        };

        private static AppUserDocumentResponse MapDocToResponse(AppUserDocument d) => new()
        {
            Id = d.Id,
            FileName = d.FileName,
            FileUrl = d.FileUrl,
            FileType = d.FileType,
            FileSize = d.FileSize,
            UploadedAt = d.CreatedAt
        };
    }
}