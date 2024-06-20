using Microsoft.Extensions.Hosting;

namespace DigitalNotes.Infrastructure.Data;

internal class DigitalNotesDbContextMigration : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DigitalNotesDbContextMigration(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dataSettings = scope.ServiceProvider.GetRequiredService<DataSettings>();
        if (!dataSettings.UseInMemory)
        {
            var context = scope.ServiceProvider.GetRequiredService<DigitalNotesDbContext>();
            await context.MigrateAsync();
        }
    }
}