using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Profiles.Models
{
    public class UserProfileCourseServiceModel : IMapFrom<Course>, ICustomMapperProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string studentId = null;

            mapper
            .CreateMap<Course, UserProfileCourseServiceModel>()
            .ForMember(p => p.Grade, cfq => cfq.MapFrom(c => c.Students
                                                              .Where(s => s.StudentId == studentId)
                                                              .Select(s => s.Grade)
                                                              .FirstOrDefault()));
        }
    }
}
