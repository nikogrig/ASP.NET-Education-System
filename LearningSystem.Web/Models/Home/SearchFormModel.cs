using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Home
{
    public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search In Users")]
        public bool SearchInUsers { get; set; } = false;

        [Display(Name = "Search In Courses")]
        public bool SearchInCourses { get; set; } = false;

        [Display(Name = "Search In Blog")]  
        public bool SearchInBlog { get; set; } = false;
    }
}
