﻿using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddPost(Post entity)
        {
            _unitOfWork.PostRepository.Add(entity);
            _unitOfWork.Commit();
        }
    }
}
