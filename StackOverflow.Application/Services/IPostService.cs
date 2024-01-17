﻿using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public interface IPostService
    {
        void AddPost(Post entity);
        Post GetById(int id);
        IList<Post> GetAllPost();
    }
}
