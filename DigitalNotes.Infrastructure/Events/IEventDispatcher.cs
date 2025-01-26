using DigitalNotes.Domain.Common;

namespace DigitalNotes.Infrastructure.Events;

internal interface IEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IDomainEvent;
}