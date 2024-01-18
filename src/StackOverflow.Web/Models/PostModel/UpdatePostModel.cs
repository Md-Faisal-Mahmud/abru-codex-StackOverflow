using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.PostModel
{
    public class UpdatePostModel
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostContent { get; set; }

        private IPostService _postService;

        public UpdatePostModel()
        {
            
        }
        public UpdatePostModel(IPostService postService)
        {
            _postService = postService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
        }

        internal async Task Load()
        {
            var data = await _postService.GetById(PostId);
            if (data != null)
            {
                this.PostId = data.Id;
                this.PostTitle = data.Title;
                this.PostContent = data.Description;
            }
        }

        internal async Task Update()
        {
            var post = await _postService.GetById(PostId);
            if (post != null)
            {
                post.Title= this.PostTitle;
                post.Description= this.PostContent;

            }

            await _postService.UpdatePost(post);

        }
    }
}
