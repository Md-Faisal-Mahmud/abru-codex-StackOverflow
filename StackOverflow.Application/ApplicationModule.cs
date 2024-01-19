using Autofac;
using StackOverflow.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>().As<IPostService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TagService>().As<ITagService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnswerService>().As<IAnswerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnswerVoteService>().As<IAnswerVoteService>()
                .InstancePerLifetimeScope(); 
        }
    }
}
