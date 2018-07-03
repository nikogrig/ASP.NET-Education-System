using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Courses.Contracts
{
    public interface ICourseEnrolledService
    {
        Task<bool> StudentIsEnrolledInCourseAsync(string userId, int courseId);

        Task<bool> SignUpStudentAsync(string userId, int courseId);

        Task<bool> SignOutStudentAsync(string studentId, int courseId);
    }
}
