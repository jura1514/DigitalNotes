namespace DigitalNotes.Domain.Common;

public abstract class EventDrivenAggregateBase
{
    private readonly List<IDomainEvent> _uncommittedEvents = [];
    public Guid Id { get; protected set; }
    public long Version { get; private set; }

    public IReadOnlyList<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents;

    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

    protected void RegisterEvent(IDomainEvent @event)
    {
        When(@event);
        _uncommittedEvents.Add(@event);
    }

    public void ApplyEvent(object @event)
    {
        When(@event);
        Version++;
    }

    protected abstract void When(object @event);
}