using System.Diagnostics;
using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Identity.Data;
using OpenTelemetry.Trace;

namespace DigitalNotes.MigrationService;

internal class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<Worker> logger)
    : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private readonly ActivitySource _sActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = _sActivitySource.StartActivity(
            $"Migrating databases for contexts: ${nameof(DigitalNotesDbContext)}, ${nameof(DigitalNotesIdentityDbContext)}",
            ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();

            await DbContextMigration<DigitalNotesDbContext>.ExecuteMigrationAsync(logger, scope.ServiceProvider,
                cancellationToken);
            await DbContextMigration<DigitalNotesIdentityDbContext>.ExecuteMigrationAsync(logger, scope.ServiceProvider,
                cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}