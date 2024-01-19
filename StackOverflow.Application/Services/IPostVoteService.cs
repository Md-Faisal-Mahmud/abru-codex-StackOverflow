using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public interface IPostVoteService
    {
        Task AddVote(PostVote entity);
        Task UpdateVote(PostVote entity);
        Task<PostVote?> GetByPostAndUser(Guid postId, Guid userId);
    }
}
