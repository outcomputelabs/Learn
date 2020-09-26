using Microsoft.Extensions.Options;
using System;

namespace Learn.Server.Data.SqlServer.Tests
{
    public sealed class DatabaseFixture
    {
        public DatabaseFixture()
        {
            ConnectionString = Environment.GetEnvironmentVariable("LEARN_TESTS_DATABASE_CONNECTION_STRING")
                ?? @"Server=(localdb)\mssqllocaldb;Database=Learn";

            SqlServerRepositoryOptions = Options.Create(new SqlServerRepositoryOptions
            {
                ConnectionString = ConnectionString
            });
        }

        public string ConnectionString { get; }

        public IOptions<SqlServerRepositoryOptions> SqlServerRepositoryOptions { get; }
    }
}