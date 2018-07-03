using LearningSystem.Constants;
using LearningSystem.Services.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;

namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    public class ArticleListingViewModel
    {
        public IEnumerable<BlogArticleListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalArticles / PAGE_SIZE);

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage <= 1
                                 ? 1
                                 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                                 ? this.TotalPages
                                 : this.CurrentPage + 1;

    }
}
