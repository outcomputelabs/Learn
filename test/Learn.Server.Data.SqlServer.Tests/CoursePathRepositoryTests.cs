using Dapper;
using Learn.Server.Data.Exceptions;
using Learn.Server.Data.SqlServer.Repositories;
using Learn.Server.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace Learn.Server.Data.SqlServer.Tests
{
    [Collection(nameof(DatabaseCollection))]
    public class CoursePathRepositoryTests
    {
        private readonly DatabaseFixture _database;

        public CoursePathRepositoryTests(DatabaseFixture database)
        {
            _database = database;
        }

        [Fact]
        public async Task GetAsyncReturnsExistingItem()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var result = await repository.GetAsync(item.Key).ConfigureAwait(false);

                // assert
                Assert.Equal(item.Key, result.Key);
                Assert.Equal(item.Name, result.Name);
                Assert.Equal(item.Slug, result.Slug);
                Assert.Equal(item.Version, result.Version);
            }
        }

        [Fact]
        public async Task GetAsyncReturnsNullOnMissingItem()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var id = Guid.NewGuid();

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var result = await repository.GetAsync(id).ConfigureAwait(false);

                // assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetAllAsyncReturnsExistingItems()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var items = new[]
                {
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid()),
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid()),
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid())
                };

                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("DELETE FROM [dbo].[CoursePath]").ConfigureAwait(false);
                    foreach (var item in items)
                    {
                        await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                    }
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var result = await repository.GetAllAsync().ConfigureAwait(false);

                // assert
                Assert.True(items.OrderBy(x => x.Key).SequenceEqual(result.OrderBy(x => x.Key)));
            }
        }

        [Fact]
        public async Task GetAllAsyncReturnsEmptyOnMissingItems()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("DELETE FROM [dbo].[CoursePath]").ConfigureAwait(false);
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var result = await repository.GetAllAsync().ConfigureAwait(false);

                // assert
                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task AddAsyncAddsNewItem()
        {
            // arrange
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());

            // act
            var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
            var result = await repository.AddAsync(item).ConfigureAwait(false);

            // assert
            using var connection = new SqlConnection(_database.ConnectionString);
            var saved = await connection.QuerySingleOrDefaultAsync<CoursePath>("SELECT [Key], [Name], [Slug], [Version] FROM [dbo].[CoursePath] WHERE [Key] = @Key", new { item.Key }).ConfigureAwait(false);

            // assert - result equals input except version
            Assert.Equal(result.Key, item.Key);
            Assert.Equal(result.Name, item.Name);
            Assert.Equal(result.Slug, item.Slug);
            Assert.NotEqual(result.Version, item.Version);

            // assert - saved equals input except version
            Assert.Equal(saved.Key, item.Key);
            Assert.Equal(saved.Name, item.Name);
            Assert.Equal(saved.Slug, item.Slug);
            Assert.NotEqual(saved.Version, item.Version);

            // assert - result version equals saved version
            Assert.Equal(saved.Version, result.Version);
        }

        [Fact]
        public async Task AddAsyncThrowsOnExistingId()
        {
            // arrange
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());

            await using (var connection = new SqlConnection(_database.ConnectionString))
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
            }

            // act
            var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
            var ex = await Assert.ThrowsAsync<KeyAlreadyExistsException>(() => repository.AddAsync(item)).ConfigureAwait(false);

            // assert
            Assert.Equal(item.Key, ex.Key);
        }

        [Fact]
        public async Task AddAsyncThrowsOnExistingName()
        {
            // arrange
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var existing = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());

            await using (var connection = new SqlConnection(_database.ConnectionString))
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", existing).ConfigureAwait(false);
            }

            // act
            var item = new CoursePath(Guid.NewGuid(), existing.Name, Guid.NewGuid().ToString(), Guid.NewGuid());
            var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
            var ex = await Assert.ThrowsAsync<NameAlreadyExistsException>(() => repository.AddAsync(item)).ConfigureAwait(false);

            // assert
            Assert.Equal(existing.Name, ex.Name);
        }

        [Fact]
        public async Task AddAsyncThrowsOnExistingSlug()
        {
            // arrange
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var existing = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());

            await using (var connection = new SqlConnection(_database.ConnectionString))
            {
                await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", existing).ConfigureAwait(false);
            }

            // act
            var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), existing.Slug, Guid.NewGuid());
            var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
            var ex = await Assert.ThrowsAsync<SlugAlreadyExistsException>(() => repository.AddAsync(item)).ConfigureAwait(false);

            // assert
            Assert.Equal(existing.Slug, ex.Slug);
        }

        [Fact]
        public async Task RemoveAsyncRemovesExistingItem()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                await repository.RemoveAsync(item.Key, item.Version).ConfigureAwait(false);

                // assert
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    var saved = await connection.QuerySingleOrDefaultAsync<CoursePath>("SELECT [Key], [Name], [Slug], [Version] FROM [dbo].[CoursePath] WHERE [Key] = @Key", new { item.Key }).ConfigureAwait(false);
                    Assert.Null(saved);
                }
            }
        }

        [Fact]
        public async Task RemoveAsyncThrowsOnVersionMismatch()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var existing = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", existing).ConfigureAwait(false);
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var item = existing.WithVersion(Guid.NewGuid());
                var ex = await Assert.ThrowsAsync<ConcurrencyException>(() => repository.RemoveAsync(item.Key, item.Version)).ConfigureAwait(false);

                // assert
                Assert.Equal(existing.Version, ex.StoredVersion);
                Assert.Equal(item.Version, ex.CurrentVersion);
            }
        }

        [Fact]
        public async Task RemoveAsyncIgnoresMissingItem()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // arrange
                var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);

                // act
                await repository.RemoveAsync(item.Key, item.Version).ConfigureAwait(false);

                // assert
                Assert.True(true);
            }
        }

        [Fact]
        public async Task UpdateAsyncUpdatesExistingItem()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var item = new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid());
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var current = await repository.GetAsync(item.Key).ConfigureAwait(false);
                var proposed = new CoursePath(current.Key, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), current.Version);
                var saved = await repository.UpdateAsync(proposed).ConfigureAwait(false);

                // assert
                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    var actual = await connection.QuerySingleOrDefaultAsync<CoursePath>("SELECT [Key], [Name], [Slug], [Version] FROM [dbo].[CoursePath] WHERE [Key] = @Key", new { item.Key }).ConfigureAwait(false);

                    // assert - current equals item
                    Assert.Equal(item, current);

                    // assert - actual equals saved
                    Assert.Equal(saved, actual);

                    // assert - saved equals proposed plus new version
                    Assert.Equal(proposed.Key, saved.Key);
                    Assert.Equal(proposed.Name, saved.Name);
                    Assert.Equal(proposed.Slug, saved.Slug);
                    Assert.NotEqual(proposed.Version, saved.Version);
                }
            }
        }

        [Fact]
        public async Task UpdateAsyncThrowsOnExistingName()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var items = new[]
                {
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid()),
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid())
                };

                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    foreach (var item in items)
                    {
                        await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                    }
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var current = await repository.GetAsync(items[0].Key).ConfigureAwait(false);
                var proposed = new CoursePath(current.Key, items[1].Name, Guid.NewGuid().ToString(), current.Version);
                var ex = await Assert.ThrowsAsync<NameAlreadyExistsException>(() => repository.UpdateAsync(proposed)).ConfigureAwait(false);

                // assert
                Assert.Equal(items[1].Name, ex.Name);
            }
        }

        [Fact]
        public async Task UpdateAsyncThrowsOnExistingSlug()
        {
            // arrange
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var items = new[]
                {
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid()),
                    new CoursePath(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid())
                };

                using (var connection = new SqlConnection(_database.ConnectionString))
                {
                    foreach (var item in items)
                    {
                        await connection.ExecuteAsync("INSERT INTO [dbo].[CoursePath] ([Key], [Name], [Slug], [Version]) VALUES (@Key, @Name, @Slug, @Version)", item).ConfigureAwait(false);
                    }
                }

                // act
                var repository = new CoursePathRepository(_database.SqlServerRepositoryOptions);
                var current = await repository.GetAsync(items[0].Key).ConfigureAwait(false);
                var proposed = new CoursePath(current.Key, Guid.NewGuid().ToString(), items[1].Slug, current.Version);
                var ex = await Assert.ThrowsAsync<SlugAlreadyExistsException>(() => repository.UpdateAsync(proposed)).ConfigureAwait(false);

                // assert
                Assert.Equal(items[1].Slug, ex.Slug);
            }
        }
    }
}