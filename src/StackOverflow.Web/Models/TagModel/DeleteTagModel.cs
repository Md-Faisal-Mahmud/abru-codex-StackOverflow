using Autofac;
using StackOverflow.Application.Services;

namespace StackOverflow.Web.Models.TagModel
{
    public class DeleteTagModel
    {
        private ITagService _tagService;

        public DeleteTagModel()
        {

        }

        public DeleteTagModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _tagService = scope.Resolve<ITagService>();
        }

        internal async Task DeleteTag(Guid id)
        {
           await _tagService.DeleteTag(id);
        }
    }
}
