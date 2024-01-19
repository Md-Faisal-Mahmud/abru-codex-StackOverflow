using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.AnswerModel;
using System.Security.Claims;

namespace StackOverflow.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly ILifetimeScope _scope;

        public VoteController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerUpdateVote(Guid answerId, string voteType)
        {
            var model = _scope.Resolve<AnswerVoteModel>();
            model.ResolveDependency(_scope);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await model.UpdateVote(answerId, Guid.Parse(userId), voteType);

                return Ok();
            }

            return BadRequest("Some thing went wrong");
        }
    }
}
