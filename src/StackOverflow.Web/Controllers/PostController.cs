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
        private IUnitOfWork _unitOfWork;
        public PostController(ILifetimeScope scope, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _scope = scope;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<PostListModel>();
            model.ResolveDependency(_scope);
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
            await model.Add();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _scope.Resolve<PostListModel>();
            model.ResolveDependency(_scope);
            var post = model.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}
