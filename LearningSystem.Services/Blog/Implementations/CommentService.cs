using AutoMapper.QueryableExtensions;
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

namespace LearningSystem.Services.Blog.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly LearningSystemDbContext db;

        public CommentService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CommentsArticleDetailsListingServiceModel>> GetAllCommentsAsync(int articleId)
        {
            var comments = await this.db
                .Comments
                .Where(a => a.ArticleId == articleId)
                .ProjectTo<CommentsArticleDetailsListingServiceModel>()
                .ToListAsync();

            return comments;
        }

        public async Task AddCommentAsync(string userId, int articleId, string description)
        {
            var comment = new Comment
            {
                Description = description,
                UserId = userId,
                ArticleId = articleId
            };

            this.db.Comments.Add(comment);

            await this.db.SaveChangesAsync();
        }
    }
}
