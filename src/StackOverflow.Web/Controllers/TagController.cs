using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.TagModel;

namespace StackOverflow.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ILifetimeScope _scope;

        public TagController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<GetTagModel>();
            model.ResolveDependency(_scope);

            await model.GetTags();

            return View(model);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTagModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.Add();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<DeleteTagModel>();
            model.ResolveDependency(_scope);

            await model.DeleteTag(id);

            return RedirectToAction("Index");
        }

    }
}
