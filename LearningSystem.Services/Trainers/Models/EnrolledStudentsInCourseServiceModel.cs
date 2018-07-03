using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Trainers.Models
{
    public class EnrolledStudentsInCourseServiceModel : IMapFrom<User>, ICustomMapperProfile
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            int courseId = default(int);

            mapper
                .CreateMap<User, EnrolledStudentsInCourseServiceModel>()
                .ForMember(s => s.Grade, cfg => cfg.MapFrom(u => u.Courses
                                                                  .Where(c => c.CourseId == courseId)
                                                                  .Select(c => c.Grade)
                                                                  .FirstOrDefault()));
        }
    }
}
