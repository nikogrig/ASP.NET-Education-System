using AutoMapper.QueryableExtensions;
using LearningSystem.Constants;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Blog.Contracts;
using LearningSystem.Services.Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;

namespace LearningSystem.Services.Blog.Implementations
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> GetAllAsync(int page = START_PAGE)
        {
            var articles = await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip(GetSkippedPage(page, PAGE_SIZE))
                .Take(PAGE_SIZE)
                .ProjectTo<BlogArticleListingServiceModel>()
                .ToListAsync();

            return articles;
        }

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            await this.db
                 .Articles
                 .AddAsync(article);

            await this.db
                .SaveChangesAsync();
        }

        public async Task<int> GetTotalArticlesCountAsync() => await this.db.Articles.CountAsync();

        public async Task<BlogArticleDetailsServiceModel> GetByIdAsync(int id)
        {
            var article = await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<BlogArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

            return article;
        }

        private int GetSkippedPage(int page, int pagesize)
        {
            var skippedpage = (page - 1) * pagesize;

            return skippedpage;
        }
    }
}
