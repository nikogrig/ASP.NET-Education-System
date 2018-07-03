using LearningSystem.Constants;
using LearningSystem.Data.Models;
using LearningSystem.Services.Admin.Contracts;
using LearningSystem.Web.Areas.Admin.Models.Courses;
using LearningSystem.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Infrastructure.TempDataMessages;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    public class CoursesController : BasicAdminController
    {
        private readonly IAdminCourseService courseService;
        private readonly UserManager<User> userManager;

        public CoursesController(IAdminCourseService courseService, UserManager<User> userManager)
        {
            this.courseService = courseService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var trainers = await this.userManager
                .GetUsersInRoleAsync(IdentitiesConstants.TRAINER_ROLE);

            var trainersListItem = await this.GetTrainersAsync();

            var model = new AddCourseFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Trainers = trainersListItem
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var trainersListItem = await this.GetTrainersAsync();

            await this.courseService
                .CreateAsync(model.Name, 
                             model.Description, 
                             model.StartDate,
                             model.EndDate.AddDays(1), 
                             model.TrainerId);

            TempData.AddSuccessMessage($"Course '{model.Name}' was created successfully!");

            return RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                new { area = string.Empty });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainersAsync()
        {
            var trainers = await this.userManager
                .GetUsersInRoleAsync(IdentitiesConstants.TRAINER_ROLE);

            var trainerListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainerListItems;
        }
    }
}
