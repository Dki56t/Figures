using Autofac;
using Implementation.DataAccess.Repositories;

namespace Implementation.DataAccess
{
    public sealed class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FigureRepository>().AsImplementedInterfaces();
        }
    }
}