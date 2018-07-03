using LearningSystem.Common.Mapping;
using LearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Services.Profiles.Models
{
    public class UserInfoServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
