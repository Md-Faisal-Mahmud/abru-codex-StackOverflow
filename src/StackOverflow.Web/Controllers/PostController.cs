using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.PostModel;
using System.Security.Claims;

namespace StackOverflow.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<PostController> _logger;
        public PostController(ILifetimeScope scope, IHttpContextAccessor contextAccessor,
            ILogger<PostController> logger)
        {
            _scope = scope;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var model = _scope.Resolve<GetPostModel>();
            model.ResolveDependency(_scope);

            model.CurrentPage = pageIndex;
            await model.GetPosts();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = _scope.Resolve<AddPostModel>();
                model.ResolveDependency(_scope);

                await model.loadTags();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("500 Internal Server Error");
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddPostModel model)
        {
            try
            {
                model.ResolveDependency(_scope);

                if (User.Identity!.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    model.CreatedByUserId = Guid.Parse(userId);
                }

                if (ModelState.IsValid)
                {
                    await model.Add();

                    return RedirectToAction("Index");
                }

                await model.loadTags();

                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = _scope.Resolve<GetPostModel>();
                model.ResolveDependency(_scope);
                var post = await model.GetPost(id);
                if (post == null)
                {
                    return NotFound();
                }

                if (User.Identity!.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    _contextAccessor.HttpContext.Session.SetString("userId", Guid.Parse(userId).ToString());
                }

                return View(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RedirectToAction("Index");
            }

        }


    }
}
