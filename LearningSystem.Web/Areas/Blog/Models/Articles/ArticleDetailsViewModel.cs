using LearningSystem.Services.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    public class ArticleDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommentsArticleDetailsListingServiceModel> Comments { get; set; }
    }
}
