using LearningSystem.Services.Profiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Profiles.Contracts
{
    public interface IProfileService
    {
        Task<UserProfileServiceModel> GetProfileByIdAsync(string userId);

        Task<UserInfoServiceModel> GetUserByIdAsync(string userId);
    }
}
