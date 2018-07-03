using LearningSystem.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.MAX_COMMENT_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
