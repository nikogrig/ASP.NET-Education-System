using LearningSystem.Services.Courses.Models;
using LearningSystem.Services.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Home
{
    public class SearchViewModel
    {
        public IEnumerable<CourseSearchServiceModel> Courses { get; set; } = new List<CourseSearchServiceModel>();

        public IEnumerable<UserSearchServiceModel> Users { get; set; } = new List<UserSearchServiceModel>();

        public IEnumerable<BlogSearchServiceModel> Blog { get; set; } = new List<BlogSearchServiceModel>();

        public string SearchText { get; set; }
    }
}
