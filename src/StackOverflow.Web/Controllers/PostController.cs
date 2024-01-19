using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Features.Membership;
using StackOverflow.Infrastructure.UnitOfWorks;
using StackOverflow.Web.Models.PostModel;

namespace StackOverflow.Web.Controllers
{
    public class PostController : Controller
    {
        private ILifetimeScope _scope;
        private UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        public PostController(ILifetimeScope scope, UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _scope = scope;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index(int pageIndex=1)
        {
            var model = _scope.Resolve<PostListModel>();
            model.ResolveDependency(_scope);

            model.CurrentPage = pageIndex;
            await model.GetPosts();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = _scope.Resolve<AddPostModel>();
            model.ResolveDependency(_scope);

            await model.loadTags();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddPostModel model)
        {
            model.ResolveDependency(_scope);
            model.CreatedByUserId = new Guid(_userManager.GetUserId(User));

            if (ModelState.IsValid)
            {
                await model.Add();

                return RedirectToAction("Index");
            }

            await model.loadTags();
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<PostListModel>();
            model.ResolveDependency(_scope);
            var post = await model.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            if(User.Identity.IsAuthenticated)
            {
                _contextAccessor.HttpContext.Session.SetString("userId", _userManager.GetUserId(User).ToString());
            }
            
            return View(post);
        }

        
    }
}
