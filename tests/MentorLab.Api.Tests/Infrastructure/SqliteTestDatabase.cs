using MentorLab.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Tests.Infrastructure;

public sealed class SqliteTestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<MentorLabDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new MentorLabDbContext(options);
        Context.Database.EnsureCreated();
    }

    public MentorLabDbContext Context { get; }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
