using Implementation.DataAccess;

namespace Tests.IntegrationTests.Fixtures
{
    public sealed class DatabaseMigrationFixture
    {
        public DatabaseMigrationFixture()
        {
            DatabaseHelper.Migrate();
        }
    }
}