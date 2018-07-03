using LearningSystem.Services.Profiles.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Profile.Models.Users
{
    public class UserCreateRoleViewModel
    {
        public UserInfoServiceModel User { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
