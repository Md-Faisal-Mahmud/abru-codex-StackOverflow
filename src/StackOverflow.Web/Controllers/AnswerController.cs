using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Membership.Entities;
using StackOverflow.Web.Models.AnswerModel;

namespace StackOverflow.Web.Controllers
{
    public class AnswerController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;
        public AnswerController(UserManager<ApplicationUser> userManager, ILifetimeScope scope)
        {
            _userManager = userManager;
            _scope = scope;
        }

        [Authorize]
        public IActionResult AddAnswer(Guid postId)
        {
            var model = _scope.Resolve<AddAnswerModel>();
            model.ResolveDependency(_scope);

            model.postId = postId;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAnswer(AddAnswerModel model)
        {
            model.ResolveDependency(_scope);

            model.userId = new Guid(_userManager.GetUserId(User));
            if (ModelState.IsValid)
            {
                await model.Add();
                return RedirectToAction("Details", "Post", new { id = model.postId });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditAnswer(Guid postId, Guid answerId)
        {
            var model = _scope.Resolve<UpdateAnswerModel>();
            model.ResolveDependency(_scope);
            model.PostId = postId;
            await model.loadAnswer(answerId);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnswer(UpdateAnswerModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                await model.UpdateAnswer();
                return RedirectToAction("Details", "Post", new { id = model.PostId });
            }

            return View(model);
        }
    }
}
