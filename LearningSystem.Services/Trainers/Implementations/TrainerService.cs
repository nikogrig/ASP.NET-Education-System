using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using LearningSystem.Services.Trainers.Contracts;
using LearningSystem.Services.Trainers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Trainers.Implementations
{
    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CoursesListingServiceModel>> GetCoursesByTrainerIdAsync(string trainerId)
        {
            var courses = await this.db
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.TrainerId == trainerId)
                .ProjectTo<CoursesListingServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<EnrolledStudentsInCourseServiceModel>> GetStudentInCourseAsync(int courseId)
        {
            var students = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students
                              .Select(s => s.Student))
                .ProjectTo<EnrolledStudentsInCourseServiceModel>(new { courseId })
                .ToListAsync();

            return students;
        }

        public async Task<bool> IsTrainerAsync(int courseId, string trainerId)
        {
            var isTrainer = await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.TrainerId == trainerId);

            return isTrainer;
        }

        public async Task<CoursesListingServiceModel> GetCourseByAsync(int courseId)
        {
            var course = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .ProjectTo<CoursesListingServiceModel>()
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task<bool> AddGradeAsync(int courseId, string userid, Grade grade)
        {
            var studentInCourse = await this.db
                .FindAsync<StudentCourse>(userid, courseId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<StudentInCourseNamesServiceModel> GetStudentInCourseNamesAsync(int courseId, string studentId)
        {
            var username = await this.db
                .Users
                .Where(u => u.Id == studentId)
                .Select(sc => sc.UserName)
                .FirstOrDefaultAsync();

            if (username == null)
            {
                return null;
            }

            var coursename = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            if (coursename == null)
            {
                return null;
            }

            var model = new StudentInCourseNamesServiceModel
            {
                UserName = username,
                CourseName = coursename
            };

            return model;
        }
    }
}
