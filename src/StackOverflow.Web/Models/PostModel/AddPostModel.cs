using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Features.Membership;

namespace StackOverflow.Web.Models.PostModel
{
    public class AddPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedByUserId { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public Guid TagsIds { get;  set; } 

        private IPostService _postService;
        private ITagService _tagService;
        private IUserService _userService;

        public AddPostModel()
        {

        }

        public AddPostModel(IPostService postService, ITagService tagService,
            IUserService userService )
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

            var postTags = new List<Tag>();
            var tag = await _tagService.GetById(TagsIds);
            if(tag != null)
            {
                postTags.Add(tag);
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
