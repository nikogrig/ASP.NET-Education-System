using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LearningSystem.Data.Models;
using LearningSystem.Data.Configuration;

namespace LearningSystem.Data
{
    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleConfig());

            builder.ApplyConfiguration(new CourseConfig());

            builder.ApplyConfiguration(new StudentCourseConfig());

            builder.ApplyConfiguration(new CommentConfig());

            base.OnModelCreating(builder);
        }
    }
}
