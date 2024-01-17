using Autofac;
using StackOverflow.Infrastructure.Repositories;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostRepository>().As<IPostRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TagRepository>().As<ITagRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
