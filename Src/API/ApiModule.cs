using Autofac;
using Implementation.DataAccess;
using Microsoft.Extensions.Configuration;

namespace API;

public sealed class ApiModule : Module
{
    private readonly IConfiguration _configuration;

    public ApiModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule(new DataAccessModule(_configuration));
    }
}