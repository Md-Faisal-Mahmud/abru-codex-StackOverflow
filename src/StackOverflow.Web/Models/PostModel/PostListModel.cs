using Autofac;
using Microsoft.Extensions.Hosting;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Web.Models.PostModel
{
    public class PostListModel
    {
        public IList<Post> Posts { get; set; } = new List<Post>();

        private IPostService _postService;

        public PostListModel()
        {

        }

        public PostListModel(IPostService postService)
        {
            _postService = postService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
        }

        public void GetPosts()
        {
            //Posts= _postService.GetAllPost();
        }

        public Post GetPost(int id)
        {
            //return _postService.GetById(id);
            throw new NotImplementedException();
        }
    }
}
