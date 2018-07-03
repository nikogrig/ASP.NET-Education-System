using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Admin.Contracts;
using LearningSystem.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Admin.Implementations
{
    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext db;

        public AdminCourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            await this.db
                .Courses
                .AddAsync(course);

            await this.db.SaveChangesAsync();
        }
    }
}
