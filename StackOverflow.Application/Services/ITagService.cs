using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Application.Services
{
    public interface ITagService
    {
        Task AddTag(Tag entity);
        Task DeleteTag(Guid id);
        Task<Tag?> GetById(Guid id);
        Task<IList<Tag>> GetAllTag();
    }
}
