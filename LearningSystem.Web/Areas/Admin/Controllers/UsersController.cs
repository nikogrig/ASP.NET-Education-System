using LearningSystem.Data.Models;
using LearningSystem.Services.Admin.Contracts;
using LearningSystem.Web.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LearningSystem.Infrastructure.TempDataMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    public class UsersController : BasicAdminController
    {
        private readonly IAdminUserService adminService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService adminService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.adminService = adminService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new UsersListingViewModel
            {
                Users = await this.adminService.GetAllUsersAsync(page),
                TotalUsers = await this.adminService.GetTotalUsersCountAsync(),
                CurrentPage = page
            };

            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this.userManager.CreateAsync(new User
            {
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                Birthdate = model.Birthdate
            }, model.Password);

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage($"User {model.Username} successfully created.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddModelErrors(result);

                return View(model);
            }
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var currentRole = await this.userManager.GetRolesAsync(user);

            var roles = currentRole
                .Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r
                })
                .ToList();

            var model = new RolesListingViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = await this.userManager.GetRolesAsync(user),
                ListRole = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DestroyRole(AddDeleteUserToRoleFormViewModel model)
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

            var result = await this.userManager
                .RemoveFromRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage($"User {user.UserName} successfully deleted '{ model.Role}' role.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddModelErrors(result);

                return View(model);
            }
        }

        public async Task<IActionResult> ChangeRoles(string id)
        {
            var user = await this.adminService
                .GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            var model = new UserChangeRoleListingViewModel
            {
                User = user,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddDeleteUserToRoleFormViewModel model)
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

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminChangePasswordViewModel
            {
                Email = user.Email,
                Username = user.UserName,

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, AdminChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage($"Password changed successfully for user {user.UserName}.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddModelErrors(result);

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new DeleteUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        public async Task<IActionResult> Destroy(string id, DeleteUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage($"User {user.UserName} successfully deleted.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddModelErrors(result);

                return View(model);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserProfileViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Birthdate = user.Birthdate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Birthdate = model.Birthdate;

            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage($"User {user.UserName} successfully updated.");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddModelErrors(result);

                return View(model);
            }
        }

        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
