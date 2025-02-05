using DigitalNotes.Domain.Common;
using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Infrastructure.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DigitalNotes.Infrastructure.BackgroundJobs;

internal class OutboxProcessor : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(IServiceProvider serviceProvider, ILogger<OutboxProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var provider = scope.ServiceProvider;

            try
            {
                var eventDispatcher = provider.GetRequiredService<IEventDispatcher>();
                var dbContext = scope.ServiceProvider.GetRequiredService<DigitalNotesDbContext>();

                var outboxEntries = await dbContext.Outbox.Where(o => !o.Processed)
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync(stoppingToken);

                foreach (var outboxEntry in outboxEntries)
                {
                    var type = Type.GetType(outboxEntry.EventType);
                    if (type != null)
                    {
                        var @event = JsonSerializer.Deserialize(outboxEntry.EventData, type);
                        if (@event is IDomainEvent domainEvent)
                        {
                            await eventDispatcher.PublishAsync(domainEvent, stoppingToken);
                            outboxEntry.Process();
                        }
                        else
                        {
                            _logger.LogWarning("Event is not a domain event: {@Event}", @event);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Event type not found: {EventType}", outboxEntry.EventType);
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing outbox");
            }

            // wait for 1 second before polling again
            await Task.Delay(1000, stoppingToken);
        }
    }
}