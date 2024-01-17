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

        internal void DeleteTag(int id)
        {
            var tag = _tagService.GetById(id); 
            if (tag == null)
            {
                throw new Exception("tag not found");
            }

            _tagService.DeleteTag(tag);
        }
    }
}
