using LearningSystem.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Admin.Contracts
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> GetAllUsersAsync(int page = 1);

        Task<AdminUserListingServiceModel> GetUserByIdAsync(string id);

        Task<int> GetTotalUsersCountAsync();
    }
}
