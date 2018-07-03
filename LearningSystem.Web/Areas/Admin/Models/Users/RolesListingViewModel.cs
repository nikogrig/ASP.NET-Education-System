using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    public class RolesListingViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<SelectListItem> ListRole { get; set; }
    }
}
