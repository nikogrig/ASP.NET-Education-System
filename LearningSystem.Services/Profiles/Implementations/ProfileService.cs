using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Services.Profiles.Contracts;
using LearningSystem.Services.Profiles.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Profiles.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly LearningSystemDbContext db;

        public ProfileService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> GetProfileByIdAsync(string userId)
        {
            var profile = await this.db
                .Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserProfileServiceModel>(new { studentId = userId })
                .FirstOrDefaultAsync();

            return profile;
        }

        public async Task<UserInfoServiceModel> GetUserByIdAsync(string userId)
        {
            var user = await this.db
                .Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserInfoServiceModel>()
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
