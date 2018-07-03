using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningSystem.Data.Models;
using LearningSystem.Services.Courses.Contracts;
using LearningSystem.Web.Models.Home;
using LearningSystem.Services.Search.Contracts;

namespace LearningSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ISearchService searchService;

        public HomeController(ICourseService courseService, ISearchService searchService)
        {
            this.courseService = courseService;
            this.searchService = searchService;
        }


        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                Courses = await this.courseService.GetActiveCourses()
            };

            return View(model);
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            if (model.SearchInCourses)
            {
                viewModel.Courses = await this.searchService.FindSearchedCoursestAsync(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                viewModel.Users = await this.searchService.FindSearchedUsersAsync(model.SearchText);
            }

            if (model.SearchInBlog)
            {
                viewModel.Blog = await this.searchService.FindSearchedBlogAsync(model.SearchText);
            }

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
