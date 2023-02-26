using Autofac;
using CustomerOrderApp.Core;
using CustomerOrderApp.Core.Repositories;
using CustomerOrderApp.Core.Services;
using CustomerOrderApp.Repository;
using CustomerOrderApp.Repository.Repositories;
using CustomerOrderApp.Repository.UnitOfWorks;
using CustomerOrderApp.Service.Mapping;
using CustomerOrderApp.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace CustomerOrderApp.API.Modules
{
    public class RepositoryServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(
                 apiAssembly,
                 repoAssembly,
                 serviceAssembly).
                 Where(x => x.Name.EndsWith("Repository")).
                 AsImplementedInterfaces().
                 InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(
             apiAssembly,
             repoAssembly,
             serviceAssembly).
             Where(x => x.Name.EndsWith("Service")).
             AsImplementedInterfaces().
             InstancePerLifetimeScope();

        }
    }
}
