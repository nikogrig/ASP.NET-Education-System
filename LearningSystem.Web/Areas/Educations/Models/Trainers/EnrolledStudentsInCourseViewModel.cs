using LearningSystem.Services.Courses.Models;
using LearningSystem.Services.Trainers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Educations.Models.Trainers
{
    public class EnrolledStudentsInCourseViewModel
    {
        public IEnumerable<EnrolledStudentsInCourseServiceModel> Students { get; set; }

        public CoursesListingServiceModel Course { get; set; }
    }
}
