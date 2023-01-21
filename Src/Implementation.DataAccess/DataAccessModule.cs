using Autofac;
using Autofac.Extensions.DependencyInjection;
using Implementation.DataAccess.Repositories;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Implementation.DataAccess
{
    public sealed class DataAccessModule : Module
    {
        private readonly IConfiguration _configuration;

        public DataAccessModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FigureRepository>().AsImplementedInterfaces();
            builder.RegisterType<ContextFactory>();

            var services = new ServiceCollection();
            services.Configure<DatabaseOptions>(_configuration.GetSection("Database"));

            builder.Populate(services);
        }
    }
}