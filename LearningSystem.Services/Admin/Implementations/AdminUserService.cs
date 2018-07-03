using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Services.Admin.Contracts;
using LearningSystem.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;


namespace LearningSystem.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> GetAllUsersAsync(int page = START_PAGE)
        {
            var users = await this.db
                .Users
                .OrderBy(u => u.UserName)
                .Skip(GetSkippedPage(page, PAGE_SIZE))
                .Take(PAGE_SIZE)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();

            return users;
        }

        public async Task<int> GetTotalUsersCountAsync() => await this.db.Users.CountAsync();

        public async Task<AdminUserListingServiceModel> GetUserByIdAsync(string id)
        {
            var user = await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<AdminUserListingServiceModel>()
                .FirstOrDefaultAsync();

            return user;
        }

        private int GetSkippedPage(int page, int pagesize)
        {
            var skippedpage = (page - 1) * pagesize;

            return skippedpage;
        }
    }
}
