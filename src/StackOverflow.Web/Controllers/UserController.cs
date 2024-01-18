using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Features.Membership;
using StackOverflow.Web.Models.AnswerModel;
using StackOverflow.Web.Models.PostModel;

namespace StackOverflow.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ILifetimeScope scope, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> MyPost()
        {
            var model = _scope.Resolve<PostListModel>();
            model.ResolveDependency(_scope);

            var currentUserId = _userManager.GetUserId(User);

            await model.GetUserPost(new Guid(currentUserId));

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<DeletePostModel>();
            model.ResolveDependency(_scope);

            await model.DeletePost(id);

            return RedirectToAction("MyPost");
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = _scope.Resolve<UpdatePostModel>();
            model.ResolveDependency(_scope);

            model.PostId = id;
            await model.Load();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostModel model)
        {
            model.ResolveDependency(_scope);
            await model.Update();

            return RedirectToAction("MyPost");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAnswer(DeleteAnswerModel model)
        {
            model.ResolveDependency(_scope);
            await model.DeleteTag(model.AnswerId);

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }
    }
}
