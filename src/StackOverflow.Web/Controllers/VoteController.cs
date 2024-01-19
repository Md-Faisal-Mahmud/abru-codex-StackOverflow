using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.VoteModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace StackOverflow.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<VoteController> _logger;
        public VoteController(ILifetimeScope scope, ILogger<VoteController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerUpdateVote(Guid answerId, string voteType)
        {
            try
            {
                var model = _scope.Resolve<VoteModel>();
                model.ResolveDependency(_scope);

                if (User.Identity!.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await model.UpdateAnswerVote(answerId, Guid.Parse(userId), voteType);

                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostUpdateVote([Required] Guid postId,[Required] string voteType)
        {
            try
            {
                var model = _scope.Resolve<VoteModel>();
                model.ResolveDependency(_scope);

                if (User.Identity!.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await model.UpdatePostVote(postId, Guid.Parse(userId), voteType);

                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("Something went wrong");
            }
        }
    }
}
