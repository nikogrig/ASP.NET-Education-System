using LearningSystem.Services.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Blog.Contracts
{
    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticleListingServiceModel>> GetAllAsync(int page = 1);

        Task<int> GetTotalArticlesCountAsync();

        Task CreateAsync(string title, string content, string authorId);

        Task<BlogArticleDetailsServiceModel> GetByIdAsync(int id);
    }
}
