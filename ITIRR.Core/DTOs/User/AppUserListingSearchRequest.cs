using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.DTOs.User
{
    public class AppUserListingSearchRequest
    {
        public string? Type { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
