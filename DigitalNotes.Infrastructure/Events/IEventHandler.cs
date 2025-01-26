using DigitalNotes.Domain.Common;

namespace DigitalNotes.Infrastructure.Events;

internal interface IEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}