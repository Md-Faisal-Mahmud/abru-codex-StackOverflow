using Autofac;
using StackOverflow.Application.Services;

namespace StackOverflow.Web.Models.PostModel
{
    public class DeletePostModel
    {
        public IPostService _postService;

        public DeletePostModel()
        {
            
        }
        public DeletePostModel(IPostService postService)
        {
            _postService = postService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
        }

        internal async Task DeletePost(Guid id)
        {
            await _postService.DeletePost(id);
        }
    }
}
