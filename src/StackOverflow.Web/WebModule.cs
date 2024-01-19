using Autofac;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.AnswerModel;
using StackOverflow.Web.Models.PostModel;
using StackOverflow.Web.Models.TagModel;
using StackOverflow.Web.Models.VoteModel;

namespace StackOverflow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<RegisterModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginModel>().AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<TagModel>().AsSelf();

            builder.RegisterType<AddPostModel>().AsSelf();
            builder.RegisterType<GetPostModel>().AsSelf();
            builder.RegisterType<DeletePostModel>().AsSelf();
            builder.RegisterType<UpdatePostModel>().AsSelf();
            builder.RegisterType<DeleteAnswerModel>().AsSelf();
            builder.RegisterType<UpdateAnswerModel>().AsSelf();

            builder.RegisterType<AddAnswerModel>().AsSelf();

            builder.RegisterType<VoteModel>().AsSelf();

            base.Load(builder);
        }
    }
}