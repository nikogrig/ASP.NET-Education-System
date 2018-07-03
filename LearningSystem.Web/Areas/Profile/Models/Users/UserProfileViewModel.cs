using LearningSystem.Services.Profiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Profile.Models.Users
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }

        public UserProfileServiceModel Profile { get; set; }
    }
}
