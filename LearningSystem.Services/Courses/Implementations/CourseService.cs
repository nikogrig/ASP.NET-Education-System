using AutoMapper.QueryableExtensions;
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
using static LearningSystem.Constants.PaginationConstants;

namespace LearningSystem.Services.Courses.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> GetAllCoursesAsync(int page = START_PAGE)
        {
            var courses = await this.db
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Skip(GetSkippedPage(page, PAGE_SIZE))
                .Take(PAGE_SIZE)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<int> GetTotalCoursesCount() => await this.db.Courses.CountAsync();

        public async Task<CourseListingServiceModel> GetCourseByIdAsync(int id)
        {
            var course = await this.db
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Id == id)
                .ProjectTo<CourseListingServiceModel>()
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> GetActiveCourses()
        {
            var courses = await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

            return courses;
        }

        private int GetSkippedPage(int page, int pagesize)
        {
            var skippedpage = (page - 1) * pagesize;

            return skippedpage;
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
