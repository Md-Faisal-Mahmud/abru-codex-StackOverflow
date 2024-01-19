using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.TagModel;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<TagController> _logger;
        public TagController(ILifetimeScope scope, ILogger<TagController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = _scope.Resolve<TagModel>();

                await model.Load();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("500 Internal Server Error");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ResolveDependency(_scope);
                    await model.Add();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return RedirectToAction("Index");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            try
            {
                var model = _scope.Resolve<TagModel>();

                await model.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return RedirectToAction("Index");
            }

        }

    }
}
