using Autofac;
using Implementation.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.IntegrationTests.Fixtures;

public sealed class DataAccessModuleFixture
{
    public DataAccessModuleFixture()
    {
        var connectionString = Configurator.ConnectionString;

        new Db(connectionString).Database.Migrate();

        var builder = new ContainerBuilder();

        builder.RegisterModule(new DataAccessModule(Configurator.Configuration));

        Container = builder.Build();
    }

    public IContainer Container { get; }
}