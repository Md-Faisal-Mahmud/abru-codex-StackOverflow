using Autofac;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Entity;
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

        public IActionResult Index()
        {
            var model = _scope.Resolve<GetTagModel>();
            model.ResolveDependency(_scope);

            model.GetTags();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddTagModel model)
        {
            if(ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                model.Add();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<DeleteTagModel>();
            model.ResolveDependency(_scope);

            model.DeleteTag(id);

            return RedirectToAction("Index");
        }

    }
}
