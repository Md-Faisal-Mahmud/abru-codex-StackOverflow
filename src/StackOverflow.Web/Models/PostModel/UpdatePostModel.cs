using Autofac;
using Ganss.Xss;
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
        [MaxLength(100)]
        [MinLength(10)]
        public string PostTitle { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(4000)]
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

            if (post == null)
            {
                throw new InvalidOperationException($"Post with ID {PostId} don't exist.");
            }

            post.Title = this.PostTitle;
            post.Description = PostContent;

            await _postService.UpdatePost(post);

        }
    }
}
