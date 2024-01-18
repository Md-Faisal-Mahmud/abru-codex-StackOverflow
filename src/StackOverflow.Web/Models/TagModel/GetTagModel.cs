using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Web.Models.TagModel
{
    public class GetTagModel
    {
        public IList<Tag> Tags { get; private set; }

        private ITagService _tagService;

        public GetTagModel()
        {

        }

        public GetTagModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _tagService = scope.Resolve<ITagService>();
        }

        internal async Task GetTags()
        {
            Tags= await _tagService.GetAllTag();
        }
    }
}
