using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Blog.Models
{
    public class CommentsArticleDetailsListingServiceModel : IMapFrom<Comment>, ICustomMapperProfile
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string User { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Comment, CommentsArticleListingServiceModel>()
                .ForMember(a => a.User, cfg => cfg.MapFrom(u => u.User.Name));
        }
    }
}
