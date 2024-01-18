using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;

namespace StackOverflow.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPost(Post entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Post.AddOrUpdateAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task<IList<Post>> GetAllPost()
        {
            return await _unitOfWork.Post.GetAllAsync();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _unitOfWork.Post.GetSingleAsync(id);
        }
    }
}
