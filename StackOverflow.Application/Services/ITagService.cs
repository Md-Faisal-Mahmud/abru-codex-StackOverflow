using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Application.Services
{
    public interface ITagService
    {
        void AddTag(Tag entity);
        Tag GetById(int id);
        IList<Tag> GetAllTag();
    }
}
