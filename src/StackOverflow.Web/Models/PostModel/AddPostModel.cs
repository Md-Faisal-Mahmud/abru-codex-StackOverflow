using Autofac;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.PostModel
{
    public class AddPostModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(4000)]
        public string Description { get; set; }
        [Required]
        public Guid CreatedByUserId { get; set; }
        public List<SelectListItem>? Tags { get; set; }
        [Required]
        public Guid TagsIds { get; set; }

        private IPostService _postService;
        private ITagService _tagService;
        private IUserService _userService;

        public AddPostModel()
        {

        }

        public AddPostModel(IPostService postService, ITagService tagService,
            IUserService userService)
        {
            _postService = postService;
            _tagService = tagService;
            _userService = userService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
            _tagService = scope.Resolve<ITagService>();
            _userService = scope.Resolve<IUserService>();
        }

        internal async Task loadTags()
        {
            var data = await _tagService.GetAllTag();

            Tags = data.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.TagName
            }).ToList();
        }

        internal async Task Add()
        {
            var user = await _userService.GetByIdAsync(CreatedByUserId);

            if (user == null)
            {
                throw new InvalidOperationException("user not found");
            }

            var tag = await _tagService.GetById(TagsIds);

            if(tag==null)
            {
                throw new InvalidOperationException($"Tag with ID {TagsIds} don't exist.");
            }


            var post = new Post
            {
                Title = Title,
                Description = Description,
                User = user,
                Tag = tag
            };

            await _postService.AddPost(post);
        }
    }
}
