using Microsoft.AspNetCore.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
