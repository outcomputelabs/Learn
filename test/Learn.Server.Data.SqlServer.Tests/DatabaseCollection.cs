using Xunit;

namespace Learn.Server.Data.SqlServer.Tests
{
    [CollectionDefinition(nameof(DatabaseCollection))]
    public sealed class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}