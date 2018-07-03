using LearningSystem.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;

namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    public class UsersListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public int TotalUsers { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalUsers / PAGE_SIZE);

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage <= 1
                                 ? 1
                                 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                                 ? this.TotalPages
                                 : this.CurrentPage + 1;
    }
}
