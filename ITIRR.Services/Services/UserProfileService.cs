using ITIRR.Core.DTOs.Profile;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;

namespace ITIRR.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadsRoot;

        public UserProfileService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _uploadsRoot = env.WebRootPath;
        }

        // ── GET ──
        public async Task<UserProfileResponse?> GetProfileAsync(string userId)
        {
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null) return null;
            return MapToResponse(profile);
        }

        // ── UPDATE BASIC ──
        public async Task<UserProfileResponse> UpdateBasicInfoAsync(
            UpdateBasicInfoRequest request, string userId)
        {
            var profile = await GetOrCreateProfile(userId);
            profile.AgencyName = request.AgencyName;
            profile.About = request.About;
            profile.Languages = request.Languages;
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(profile);
        }

        // ── UPDATE CONTACT ──
        public async Task<UserProfileResponse> UpdateContactAsync(
            UpdateContactRequest request, string userId)
        {
            var profile = await GetOrCreateProfile(userId);
            profile.PhoneNumber = request.PhoneNumber;
            profile.Email = request.Email;
            profile.Facebook = request.Facebook;
            profile.Twitter = request.Twitter;
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(profile);
        }

        // ── UPLOAD PROFILE PHOTO ──
        public async Task<UserProfileResponse> UpdateProfilePhotoAsync(
            Stream photo, string fileName, string userId)
        {
            var folder = Path.Combine(_uploadsRoot, "uploads", "profile-photos");
            Directory.CreateDirectory(folder);

            var ext = Path.GetExtension(fileName);
            var newName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(folder, newName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
                await photo.CopyToAsync(fs);

            var profile = await GetOrCreateProfile(userId);
            profile.ProfilePhotoUrl = $"/uploads/profile-photos/{newName}";
            profile.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToResponse(profile);
        }

        // ── GET DOCUMENTS ──
        public async Task<IEnumerable<UserDocumentResponse>> GetDocumentsAsync(string userId)
        {
            return await _context.UserDocuments
                .Where(d => d.UserId == userId && !d.IsDeleted)
                .OrderByDescending(d => d.UploadedAt)
                .Select(d => new UserDocumentResponse
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    FileUrl = d.FileUrl,
                    FileType = d.FileType,
                    FileSize = d.FileSize,
                    UploadedAt = d.UploadedAt
                })
                .ToListAsync();
        }

        // ── UPLOAD DOCUMENT ──
        public async Task<UserDocumentResponse> UploadDocumentAsync(
            Stream file, string fileName, long fileSize, string userId)
        {
            var folder = Path.Combine(_uploadsRoot, "uploads", "user-documents");
            Directory.CreateDirectory(folder);

            var ext = Path.GetExtension(fileName).ToLower();
            var newName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(folder, newName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(fs);

            var fileType = ext == ".pdf" ? "pdf" : "image";

            var doc = new UserDocument
            {
                UserId = userId,
                FileName = fileName,
                FileUrl = $"/uploads/user-documents/{newName}",
                FileType = fileType,
                FileSize = fileSize,
                UploadedAt = DateTime.UtcNow
            };

            _context.UserDocuments.Add(doc);
            await _context.SaveChangesAsync();

            return new UserDocumentResponse
            {
                Id = doc.Id,
                FileName = doc.FileName,
                FileUrl = doc.FileUrl,
                FileType = doc.FileType,
                FileSize = doc.FileSize,
                UploadedAt = doc.UploadedAt
            };
        }

        // ── DELETE DOCUMENT ──
        public async Task DeleteDocumentAsync(Guid documentId, string userId)
        {
            var doc = await _context.UserDocuments
                .FirstOrDefaultAsync(d => d.Id == documentId && d.UserId == userId);
            if (doc != null)
            {
                doc.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        // ── HELPERS ──
        private async Task<UserProfile> GetOrCreateProfile(string userId)
        {
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                profile = new UserProfile { UserId = userId };
                _context.UserProfiles.Add(profile);
            }
            return profile;
        }

        private static UserProfileResponse MapToResponse(UserProfile p) => new()
        {
            Id = p.Id,
            AgencyName = p.AgencyName,
            About = p.About,
            Languages = p.Languages,
            ProfilePhotoUrl = p.ProfilePhotoUrl,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Facebook = p.Facebook,
            Twitter = p.Twitter
        };
    }
}