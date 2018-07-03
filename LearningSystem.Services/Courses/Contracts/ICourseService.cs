using LearningSystem.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Courses.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> GetAllCoursesAsync(int page = 1);

        Task<int> GetTotalCoursesCount();

        Task<CourseListingServiceModel> GetCourseByIdAsync(int id);

        Task<IEnumerable<CourseListingServiceModel>> GetActiveCourses();
    }
}
