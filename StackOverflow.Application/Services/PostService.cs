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

        public async Task DeletePost(Guid id)
        {
            await _unitOfWork.BeginTransaction();

            var postEntity = await GetById(id);

            if (postEntity == null)
            {
                throw new InvalidOperationException("post not found");
            }
            await _unitOfWork.Post.DeleteAsync(postEntity);
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

        public async Task<IList<Post>> GetUserPost(Guid userId)
        {
            return await _unitOfWork.Post.FindAsync(x=>x.User.Id==userId);
        }

        public async Task UpdatePost(Post entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Post.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }
    }
}
