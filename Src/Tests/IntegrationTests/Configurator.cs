using System;
using Microsoft.Extensions.Configuration;

internal static class Configurator
{
    static Configurator()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.test.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        ConnectionString = Configuration["Database:ConnectionString"];
    }

    public static IConfiguration Configuration { get; }
    public static string ConnectionString { get; }
}