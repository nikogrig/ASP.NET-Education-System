using LearningSystem.Data.Enums;
using LearningSystem.Data.Models;
using LearningSystem.Services.Courses.Implementations;
using LearningSystem.Services.Trainers.Contracts;
using LearningSystem.Web.Areas.Educations.Models.Trainers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Infrastructure.TempDataMessages;
using LearningSystem.Services.Files.Contracts;

namespace LearningSystem.Web.Areas.Educations.Controllers
{
    public class TrainersController : BaseTrainersController
    {
        private readonly ITrainerService trainerService;
        private readonly UserManager<User> userManager;
        private readonly IFileService fileService;

        public TrainersController(ITrainerService trainersService, UserManager<User> userManager, IFileService fileService)
        {
            this.trainerService = trainersService;
            this.userManager = userManager;
            this.fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            var model = new CoursesListingViewModel
            {
                Courses = await this.trainerService.GetCoursesByTrainerIdAsync(userId)
            };

            return View(model);
        }

        public async Task<IActionResult> Students(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            var isTrainers = await this.trainerService.IsTrainerAsync(courseId, userId);

            if (!isTrainers)
            {
                return BadRequest();
            }

            var students = await this.trainerService.GetStudentInCourseAsync(courseId);

            var course = await this.trainerService.GetCourseByAsync(courseId);

            var model = new EnrolledStudentsInCourseViewModel
            {
                Students = students,
                Course = course
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(int courseId, string studentId, Grade grade)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            var isTrainers = await this.trainerService.IsTrainerAsync(courseId, userId);

            if (!isTrainers)
            {
                return BadRequest();
            }

            var result = await this.trainerService.AddGradeAsync(courseId, studentId, grade);

            if (result)
            {
                TempData.AddSuccessMessage($"Trainer successfully added grade '{grade.ToString()}'.");

                return RedirectToAction(nameof(Students), new { courseId });
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> DownLoadExam(int courseId, string studentId)
        {
            if (String.IsNullOrEmpty(studentId))
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);

            if (!await this.trainerService.IsTrainerAsync(courseId, userId))
            {
                return BadRequest();
            }

            var examContent = await this.fileService.GetExamSubmissionAsync(studentId, courseId);

            if (examContent == null)
            {
                TempData.AddErrorMessage($"The student don't upload the Exam");

                return RedirectToAction(nameof(Students), new { courseId });
            }

            var studentInCourseNames = await this.trainerService
                .GetStudentInCourseNamesAsync(courseId, studentId);

            if (studentInCourseNames == null)
            {
                return BadRequest();
            }

            return File(examContent, "application/zip", $"{studentInCourseNames.CourseName}-{studentInCourseNames.UserName}-{DateTime.UtcNow.ToString("MM-dd-yyyy")}.zip");
        }
    }
}
