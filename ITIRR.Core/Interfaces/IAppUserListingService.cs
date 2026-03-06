using ITIRR.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IAppUserListingService
    {
        Task<(List<AppUserListingCardResponse> Items, int TotalCount)> SearchListingsAsync(
            AppUserListingSearchRequest request);

        Task<AppUserListingDetailResponse?> GetListingDetailAsync(Guid listingId);
    }
}
