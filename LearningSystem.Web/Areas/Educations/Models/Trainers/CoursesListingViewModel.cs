using LearningSystem.Services.Trainers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Educations.Models.Trainers
{
    public class CoursesListingViewModel
    {
        public IEnumerable<CoursesListingServiceModel> Courses { get; set; }
    }
}
