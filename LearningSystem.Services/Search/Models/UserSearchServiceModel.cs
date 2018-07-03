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
    public class UserSearchServiceModel : IMapFrom<User>, ICustomMapperProfile
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public int Courses { get; set; }


        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<User, UserSearchServiceModel>()
                .ForMember(u => u.Courses, cfg => cfg.MapFrom(u => u.Courses.Count));
        }
    }
}
