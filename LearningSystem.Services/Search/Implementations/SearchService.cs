using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Services.Search.Contracts;
using LearningSystem.Services.Search.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Search.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly LearningSystemDbContext db;

        public SearchService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseSearchServiceModel>> FindSearchedCoursestAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            var searchedText = await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<CourseSearchServiceModel>()
                .ToListAsync();

            return searchedText;
        }

        public async Task<IEnumerable<UserSearchServiceModel>> FindSearchedUsersAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            var searchedText = await this.db
                .Users
                .OrderBy(u => u.Name)
                .Where(u => u.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<UserSearchServiceModel>()
                .ToListAsync();

            return searchedText;
        }

        public async Task<IEnumerable<BlogSearchServiceModel>> FindSearchedBlogAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            var searchedText = await this.db
                .Articles
                .OrderByDescending(u => u.Id)
                .Where(u => u.Title.ToLower().Contains(searchText.ToLower()) || u.Content.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<BlogSearchServiceModel>()
                .ToListAsync();

            return searchedText;
        }
    }
}
