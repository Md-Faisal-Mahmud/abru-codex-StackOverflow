using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class PostController : Controller
    {
        private ILifetimeScope _scope;

        public PostController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
