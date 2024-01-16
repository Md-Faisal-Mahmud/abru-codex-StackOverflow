using Autofac;
using NHibernate.Mapping.ByCode.Impl;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Web.Models.PostModel
{
    public class AddPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedByUserId { get; set; }

        private IPostService _postService;

        public AddPostModel()
        {
            
        }

        public AddPostModel(IPostService postService)
        {
            _postService = postService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
        }

        internal void Add()
        {
            var post = new Post
            {
                Title = Title,
                Description = Description,
                CreatedByUserId = CreatedByUserId
            };

            _postService.AddPost(post);
        }
    }
}
