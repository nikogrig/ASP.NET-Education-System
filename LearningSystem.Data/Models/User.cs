using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Constants;
using Microsoft.AspNetCore.Identity;

namespace LearningSystem.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public DateTime Birthdate { get;  set; }

        public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();

        public ICollection<Course> Trainings { get; set; } = new HashSet<Course>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
