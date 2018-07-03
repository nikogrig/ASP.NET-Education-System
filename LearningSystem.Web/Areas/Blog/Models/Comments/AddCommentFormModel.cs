using LearningSystem.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Blog.Models.Comments
{
    public class AddCommentFormModel
    {
        //public int Id { get; set; }

        public int ArticleId { get; set; }

        [Required]
        [MinLength(DataConstants.MAX_COMMENT_DESCRIPTION_LENGTH)]
        public string Description { get; set; }
    }
}
