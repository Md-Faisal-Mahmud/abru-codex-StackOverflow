using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.TagModel
{
    public class AddTagModel
    {
        [DisplayName("Tag Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string TagName { get; set; }

        [DisplayName("Tag Description")]
        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string TagDescription { get; set; }

        private ITagService _tagService;

        public AddTagModel()
        {

        }

        public AddTagModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _tagService = scope.Resolve<ITagService>();
        }

        internal async Task Add()
        {
            var tag = new Tag
            {
                TagName = TagName,
                TagDescription = TagDescription
            };

            await _tagService.AddTag(tag);
        }
    }
}
