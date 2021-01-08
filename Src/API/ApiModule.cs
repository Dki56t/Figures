using Autofac;
using Implementation.DataAccess;

namespace API
{
    public sealed class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccessModule>();
        }
    }
}