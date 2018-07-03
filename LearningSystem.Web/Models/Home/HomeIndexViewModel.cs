using LearningSystem.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Home
{
    public class HomeIndexViewModel : SearchFormModel
    {
        public IEnumerable<CourseListingServiceModel> Courses { get; set; }
    }
}
