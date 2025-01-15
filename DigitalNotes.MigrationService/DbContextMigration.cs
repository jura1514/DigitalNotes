using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalNotes.MigrationService;

internal static class DbContextMigration<TDbContext> where TDbContext : DbContext
{
    public static async Task ExecuteMigrationAsync(ILogger logger, IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting migrations for database context: {DbContextType}", typeof(TDbContext).Name);
        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
        await EnsureDatabaseAsync(dbContext, cancellationToken);
        await RunMigrationAsync(dbContext, cancellationToken);
    }

    private static async Task EnsureDatabaseAsync(TDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(TDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}