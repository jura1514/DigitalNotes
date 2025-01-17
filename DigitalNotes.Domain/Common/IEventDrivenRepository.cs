namespace DigitalNotes.Domain.Common;

public interface IEventDrivenRepository<TEventDrivenAggregate>
    where TEventDrivenAggregate : EventDrivenAggregateBase
{
    Task<TEventDrivenAggregate> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default);
    Task SaveAsync(TEventDrivenAggregate aggregate, CancellationToken cancellationToken = default);
}