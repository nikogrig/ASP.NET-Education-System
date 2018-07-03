using LearningSystem.Data.Models;
using LearningSystem.Services.Courses.Contracts;
using LearningSystem.Web.Areas.Modules.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningSystem.Constants.PaginationConstants;
using LearningSystem.Infrastructure.TempDataMessages;
using LearningSystem.Constants;
using Microsoft.AspNetCore.Http;
using LearningSystem.Infrastructure.FileForm;
using LearningSystem.Services.Files.Contracts;

namespace LearningSystem.Web.Areas.Modules.Controllers
{
    public class CoursesController : BaseModulesController
    {
        private readonly ICourseService courseService;
        private readonly ICourseEnrolledService enrolledService;
        private readonly UserManager<User> userManager;
        private readonly IFileService fileService;

        public CoursesController(ICourseService courseService, ICourseEnrolledService enrolledService, UserManager<User> userManager, IFileService fileService)
        {
            this.courseService = courseService;
            this.enrolledService = enrolledService;
            this.userManager = userManager;
            this.fileService = fileService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new CoursesListingViewModel
            {
                Courses = await this.courseService.GetAllCoursesAsync(page),

                TotalCourses = await this.courseService.GetTotalCoursesCount(),

                CurrentPage = page
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int courseId)
        {
            var course = await this.courseService.GetCourseByIdAsync(courseId);

            var model = new CoursesDetailsViewModel
            {
                Course = course
            };

            if (model.Course == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var studentUserId = this.userManager.GetUserId(User);

                model.UserIsEnrolledCourse = await this.enrolledService.StudentIsEnrolledInCourseAsync(studentUserId, courseId);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUp(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var successSignedUp = await this.enrolledService.SignUpStudentAsync(userId, courseId);

            if (!successSignedUp)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Thank you for your registration!");

            return RedirectToAction(nameof(Details), new { courseId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var successSignedUp = await this.enrolledService.SignOutStudentAsync(userId, courseId);

            if (!successSignedUp)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Sorry to see you live!");

            return RedirectToAction(nameof(Details), new { courseId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitExam(int courseId, IFormFile exam)
        {
            if (!exam.FileName.EndsWith("zip")
                || exam.Length > DataConstants.COURSE_EXAM_SUBMISSION_FILE_LENGTH)
            {
                TempData.AddErrorMessage("Your file should be a '.zip' file with no more 2 MB size!");

                return RedirectToAction(nameof(Details), new { courseId });
            }

            var fileContents = await exam.ToByteArrayAsync();

            var userId = this.userManager.GetUserId(User);

            var success = await this.fileService.SaveExamSubmissionAsync(courseId, userId, fileContents);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Exam file saved successfully!");

            return RedirectToAction(nameof(Details), new { courseId });
        }
    }
}
