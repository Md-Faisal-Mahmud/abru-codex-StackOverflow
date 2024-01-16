using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Repositories;
using StackOverflow.Infrastructure.UnitOfWorks;
using StackOverflow.Web.Models;
using System.Diagnostics;

namespace StackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository postRepository;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            var post = new Post
            {
                Title = "Homecoming 2",
                CreatedByUserId = Guid.NewGuid()
            };

            try
            {
                _unitOfWork.PostRepository.Add(post);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
