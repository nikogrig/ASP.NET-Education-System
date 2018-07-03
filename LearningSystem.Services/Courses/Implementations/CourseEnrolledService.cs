using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Courses.Contracts;
using LearningSystem.Services.Courses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Courses.Implementations
{
    public class CourseEnrolledService : ICourseEnrolledService
    {
        private readonly LearningSystemDbContext db;

        public CourseEnrolledService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> StudentIsEnrolledInCourseAsync(string userId, int courseId)
        {
            var isEnrolled = await this.db
                .Courses
                .AnyAsync(c => c.Students.Any(s => s.StudentId == userId) && c.Id == courseId);

            return isEnrolled;
        }

        public async Task<bool> SignUpStudentAsync(string userId, int courseId)
        {
            var courseInfo = await this.GetCourseInfoAsync(userId, courseId);

            if (courseInfo == null
                || courseInfo.StartDate < DateTime.UtcNow
                || courseInfo.UserIsEnrolledInCourse)
            {
                return false;
            }

            var studentInCourse = new StudentCourse
            {
                StudentId = userId,
                CourseId = courseId
            };

            await this.db.AddAsync(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutStudentAsync(string studentId, int courseId)
        {
            var courseInfo = await this.GetCourseInfoAsync(studentId, courseId);

            if (courseInfo == null
                || courseInfo.StartDate < DateTime.UtcNow
                || !courseInfo.UserIsEnrolledInCourse)
            {
                return false;
            }

            // var studentInCourse = await this.db
            //     .Courses
            //     .Where(c => c.Id == courseId)
            //     .SelectMany(c => c.Students)
            //     .FirstOrDefaultAsync(s => s.StudentId == studentId);    

            var studentInCourse = await this.db
                 .FindAsync<StudentCourse>(studentId, courseId);

            this.db.Remove(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<CourseWithStudentInfo> GetCourseInfoAsync(string studentId, int courseId)
        {
            var courseInfo = await this.db
                  .Courses
                  .Where(c => c.Id == courseId)
                  .Select(c => new CourseWithStudentInfo
                  {
                      StartDate = c.StartDate,
                      UserIsEnrolledInCourse = c.Students.Any(s => s.StudentId == studentId)
                  })
                  .FirstOrDefaultAsync();

            return courseInfo;
        }
    }
}
