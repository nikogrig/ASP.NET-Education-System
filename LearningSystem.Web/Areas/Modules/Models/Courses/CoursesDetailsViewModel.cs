using LearningSystem.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Modules.Models.Courses
{
    public class CoursesDetailsViewModel
    {
        public CourseListingServiceModel Course { get; set; }

        public bool UserIsEnrolledCourse { get; set; }
    }
}
