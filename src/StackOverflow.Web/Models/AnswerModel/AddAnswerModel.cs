﻿using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.AnswerModel
{
    public class AddAnswerModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(4000)]
        public string Content { get; set; }
        public Guid postId { get; set; }
        public  Guid userId { get; set; }

        private IAnswerService _answerService;
        private IPostService _postService;
        private IUserService _userService;

        public AddAnswerModel()
        {
            
        }

        
        public AddAnswerModel(IAnswerService answerService,IPostService postService,
            IUserService userService)
        {
            _answerService = answerService;
            _postService = postService;
            _userService = userService;
        }


        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
            _answerService = scope.Resolve<IAnswerService>();
            _userService = scope.Resolve<IUserService>();
        }

        internal async Task Add()
        {
            var user = await _userService.GetByIdAsync(userId);
            var post = await _postService.GetById(postId);

            if(user == null || post == null) 
            {
                throw new InvalidOperationException();
            }

            var answer = new Answer
            {
                AnswerText = Content,
                Post = post,
                User = user
            };

            await _answerService.AddAnswer(answer);
        }
    }
}
