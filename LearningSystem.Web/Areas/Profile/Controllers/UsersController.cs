using LearningSystem.Constants;
using LearningSystem.Data.Models;
using LearningSystem.Services.Files.Contracts;
using LearningSystem.Services.Profiles.Contracts;
using LearningSystem.Web.Areas.Profile.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Infrastructure.TempDataMessages;

namespace LearningSystem.Web.Areas.Profile.Controllers
{
    [Area(AdminConstants.PROFILE_AREA)]
    public class UsersController : Controller
    {
        private readonly IProfileService profileService;
        private readonly UserManager<User> userManager;
        private readonly IFileService fileService;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IProfileService profileService, UserManager<User> userManager, IFileService fileService, RoleManager<IdentityRole> roleManager)
        {
            this.profileService = profileService;
            this.userManager = userManager;
            this.fileService = fileService;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                UserId = user.Id,
                Profile = await this.profileService.GetProfileByIdAsync(user.Id)
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var certificateContents = await this.fileService.GetPdfCertificateAsync(userId, id);

            if (certificateContents == null)
            {
                return BadRequest();
            }

            return File(certificateContents, "application/pdf", "Certificate.pdf");
        }

        [Authorize]
        public async Task<IActionResult> CreateRole(string id)
        {
            var user = await this.profileService
                .GetUserByIdAsync(id);

            var roles = await this.roleManager
                .Roles
                .Where(r => r.Name != IdentitiesConstants.ADMINISTRATOR_ROLE)
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            var model = new UserCreateRoleViewModel
            {
                User = user,
                Roles = roles 
            };


            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserCreateRoleFormViewModel model)
        {
            var user = await this.userManager
                .FindByIdAsync(model.UserId);

            var roleExists = await this.roleManager
                .RoleExistsAsync(model.Role);

            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid Idenity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role. You have to login again, before using a new role setting");

            return RedirectToAction(nameof(Index), new { user.UserName });

        }
    }
}
