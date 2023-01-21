using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Implementation.DataAccess;

public sealed class ContextFactory
{
    private readonly IOptionsSnapshot<DatabaseOptions> _options;

    public ContextFactory(IOptionsSnapshot<DatabaseOptions> options)
    {
        _options = options;
    }

    public Db CreateContext()
    {
        return new Db(_options.Value.ConnectionString);
    }
}