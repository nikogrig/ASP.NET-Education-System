using LearningSystem.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;

namespace LearningSystem.Web.Areas.Modules.Models.Courses
{
    public class CoursesListingViewModel
    {
        public IEnumerable<CourseListingServiceModel> Courses { get; set; }

        public int TotalCourses { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalCourses / PAGE_SIZE);

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
