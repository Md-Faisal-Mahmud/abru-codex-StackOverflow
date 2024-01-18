using Autofac;
using StackOverflow.Application.Services;

namespace StackOverflow.Web.Models.AnswerModel
{
    public class DeleteAnswerModel
    {
        public Guid AnswerId { get; set; }
        public Guid PostId { get; set; }

        private IAnswerService _answerService;
        public DeleteAnswerModel()
        {
            
        }
        public DeleteAnswerModel(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
        }

        internal async Task DeleteTag(Guid id)
        {
            await _answerService.DeleteTag(id);
        }
    }
}
