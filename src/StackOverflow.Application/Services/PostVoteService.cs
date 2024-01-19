using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public class PostVoteService : IPostVoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostVoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddVote(PostVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.PostVote.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public Task<PostVote?> GetByPostAndUser(Guid postId, Guid userId)
        {
            return _unitOfWork.PostVote.GetSingleAsync(x => x.Post.Id == postId && x.User.Id == userId);
        }

        public async Task UpdateVote(PostVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.PostVote.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }
    }
}
