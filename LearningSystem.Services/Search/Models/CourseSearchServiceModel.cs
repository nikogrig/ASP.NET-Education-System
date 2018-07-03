using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Search.Models
{
    public class CourseSearchServiceModel : IMapFrom<Course>, ICustomMapperProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Trainer { get; set; }

        public int StudentsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Course, CourseSearchServiceModel>()
                .ForMember(a => a.Trainer, cfq => cfq.MapFrom(a => a.Trainer.UserName))
                .ForMember(a => a.StudentsCount, cfq => cfq.MapFrom(s => s.Students.Count));
        }
    }
}
