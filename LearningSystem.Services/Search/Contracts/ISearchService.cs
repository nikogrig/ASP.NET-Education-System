using LearningSystem.Services.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Search.Contracts
{
    public interface ISearchService
    {
        Task<IEnumerable<CourseSearchServiceModel>> FindSearchedCoursestAsync(string searchText);

        Task<IEnumerable<UserSearchServiceModel>> FindSearchedUsersAsync(string searchText);

        Task<IEnumerable<BlogSearchServiceModel>> FindSearchedBlogAsync(string searchText);

    }
}
