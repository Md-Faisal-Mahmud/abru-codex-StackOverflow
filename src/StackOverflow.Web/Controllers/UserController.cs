using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Membership.Entities;
using StackOverflow.Web.Models.AnswerModel;
using StackOverflow.Web.Models.PostModel;
using System.Security.Claims;

namespace StackOverflow.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UserController> _logger;
        public UserController(ILifetimeScope scope, ILogger<UserController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> MyPost()
        {
            var model = _scope.Resolve<PostModel>();
            model.ResolveDependency(_scope);

            if (User.Identity!.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await model.GetUserPost(Guid.Parse(userId));

                return View(model);
            }

            return Unauthorized();
            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<PostModel>();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdatePostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ResolveDependency(_scope);
                    await model.Update();

                    return RedirectToAction("MyPost");
                }
                return View(model);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("500 Internal Server Error");
            }
            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAnswer(DeleteAnswerModel model)
        {
            model.ResolveDependency(_scope);
            await model.DeleteTag(model.AnswerId);

            return RedirectToAction("Details", "Post", new { id = model.PostId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptAnswer(Guid answerId, Guid postId)
        {
            var model = _scope.Resolve<UpdateAnswerModel>();
            await model.AcceptAnswer(answerId, postId);
            return Ok();
        }
    }
}
