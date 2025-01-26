using DigitalNotes.Domain.Common;

namespace DigitalNotes.Infrastructure.Events;

internal class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent
    {
        var handler = _serviceProvider.GetRequiredService<IEventHandler<TEvent>>();
        await handler.Handle(@event, cancellationToken);
    }
}