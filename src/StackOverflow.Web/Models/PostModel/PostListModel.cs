﻿using Autofac;
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

        public async Task GetPosts()
        {
            var data = await _postService.GetAllPost();

            Posts = data.Select(post => new Post
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description.Length > 200 ? post.Description.Substring(0, 200) : post.Description,
                CreatedDate = post.CreatedDate,
                Tag = post.Tag,
                User = post.User
            }).ToList();
        }

        public Post GetPost(int id)
        {
            //return _postService.GetById(id);
            throw new NotImplementedException();
        }
    }
}
