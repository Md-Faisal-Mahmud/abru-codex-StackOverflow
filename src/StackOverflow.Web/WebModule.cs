using Autofac;
using StackOverflow.Web.Models;

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

            base.Load(builder);
        }
    }
}