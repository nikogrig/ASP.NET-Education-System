using LearningSystem.Services.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Blog.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentsArticleDetailsListingServiceModel>> GetAllCommentsAsync(int articleId);

        Task AddCommentAsync(string userId, int articleId, string description);

    }
}
