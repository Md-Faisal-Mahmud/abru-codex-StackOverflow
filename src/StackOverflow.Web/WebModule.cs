using Autofac;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.PostModel;
using StackOverflow.Web.Models.TagModel;

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

            builder.RegisterType<AddPostModel>().AsSelf();
            builder.RegisterType<PostListModel>().AsSelf();
            builder.RegisterType<AddTagModel>().AsSelf();
            builder.RegisterType<GetTagModel>().AsSelf();
            builder.RegisterType<DeleteTagModel>().AsSelf();

            base.Load(builder);
        }
    }
}