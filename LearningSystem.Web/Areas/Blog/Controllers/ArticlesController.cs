using LearningSystem.Data.Models;
using LearningSystem.Infrastructure.Filters;
using LearningSystem.Services.Blog.Contracts;
using LearningSystem.Services.HTML.Contracts;
using LearningSystem.Web.Areas.Blog.Models.Articles;
using LearningSystem.Infrastructure.TempDataMessages;
using LearningSystem.Infrastructure.ControllerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Constants;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    public class ArticlesController : BaseArticleController
    {
        private readonly IHtmlService htmlService;
        private readonly IBlogArticleService articleService;
        private readonly UserManager<User> userManager;
        private readonly ICommentService commentService;

        public ArticlesController(IHtmlService htmlService, IBlogArticleService articleService, UserManager<User> userManager, ICommentService commentService)
        {
            this.htmlService = htmlService;
            this.articleService = articleService;
            this.userManager = userManager;
            this.commentService = commentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new ArticleListingViewModel
            {
                Articles = await this.articleService.GetAllAsync(page),
                TotalArticles = await this.articleService.GetTotalArticlesCountAsync(),
                CurrentPage = page
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articleService.GetByIdAsync(id);

            var model = new ArticleDetailsViewModel
            {
                Id = id,
                Title = article.Title,
                Content = article.Content,
                PublishDate = article.PublishDate,
                Author = article.Author,
                Comments = await this.commentService.GetAllCommentsAsync(id),
            };

            return this.ViewOrNotFound(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(int id, ArticleDetailsViewModel model)
        {
            model.Content = this.htmlService.DoSanitize(model.Description);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest();
            }

            await this.commentService.AddCommentAsync(userId, model.Id, model.Description);

            TempData.AddSuccessMessage("User successfully added a comment.");

            return RedirectToAction(nameof(Details), new { model.Id, model.Title });
        }

        [Authorize(Roles = IdentitiesConstants.AUTHOR_ROLE)]
        public IActionResult Create() => View();

        [Authorize(Roles = IdentitiesConstants.AUTHOR_ROLE)]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            model.Content = this.htmlService.DoSanitize(model.Content);

            var authorId = this.userManager.GetUserId(User);

            await this.articleService.CreateAsync(model.Title, model.Content, authorId);

            TempData.AddSuccessMessage($"Article {model.Title} successfully published.");

            return RedirectToAction(nameof(Index));
        }
    }
}
