using LearningSystem.Data.Enums;
using LearningSystem.Services.Courses.Models;
using LearningSystem.Services.Trainers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Trainers.Contracts
{
    public interface ITrainerService
    {
        Task<IEnumerable<CoursesListingServiceModel>> GetCoursesByTrainerIdAsync(string trainerId);

        Task<bool> IsTrainerAsync(int courseId, string trainerId);

        Task<IEnumerable<EnrolledStudentsInCourseServiceModel>> GetStudentInCourseAsync(int courseId);

        Task<CoursesListingServiceModel> GetCourseByAsync(int courseId);

        Task<bool> AddGradeAsync(int courseId, string userid, Grade grade);

        Task<StudentInCourseNamesServiceModel> GetStudentInCourseNamesAsync(int courseId, string studentId);

    }
}
