using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Application.Services
{
    public interface IVoteService
    {
        Task AddAnswerVote(AnswerVote entity);
        Task AddPostVote(PostVote entity);
        Task UpdateAnswerVote(AnswerVote entity);
        Task UpdatePostVote(PostVote entity);
        Task<AnswerVote?> GetByAnswerAndUser(Guid answerId, Guid userId);
        Task<PostVote?> GetByPostAndUser(Guid postId, Guid userId);
        Task DeleteAnswerVote(Guid voteId);
        Task DeletePostVote(Guid voteId);
    }
}
