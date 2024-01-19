using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.AnswerModel
{
    public class UpdateAnswerModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(4000)]
        public string AnswerContent { get; set; }
        [Required]
        public Guid AnswerId { get; set; }
        [Required]
        public Guid PostId { get; set; }

        private IAnswerService _answerService;

        public UpdateAnswerModel(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public UpdateAnswerModel()
        {
            
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
        }

        internal async Task loadAnswer(Guid answerId)
        {
            var answer = await _answerService.GetAnswerById(answerId);
            if(answer == null)
            {
                throw new InvalidOperationException("answer not found");
            }

            AnswerId = answer.Id;
            AnswerContent = answer.AnswerText;
        }

        internal async Task UpdateAnswer()
        {
            var answer = await _answerService.GetAnswerById(AnswerId);
            if (answer == null)
            {
                throw new InvalidOperationException("answer not found");
            }

            answer.AnswerText = AnswerContent;

            await _answerService.Update(answer);
        }

    }
}
