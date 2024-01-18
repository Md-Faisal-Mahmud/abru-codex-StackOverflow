using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Features.Membership;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAnswer(Guid postId,string answer)
        {
            var model = _scope.Resolve<AddAnswerModel>();
            model.ResolveDependency(_scope);

            model.Content = answer;
            model.userId = new Guid(_userManager.GetUserId(User));
            model.postId = postId;

            await model.Add();
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
