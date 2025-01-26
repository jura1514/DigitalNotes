using DigitalNotes.Domain.Common;
using DigitalNotes.Domain.NoteAggregate.Interfaces;
using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Infrastructure.Data.Repositories;
using DigitalNotes.Infrastructure.Events;
using Microsoft.Extensions.Hosting;

namespace DigitalNotes.Infrastructure;

public static class InfrastructureRegistration
{
    public static IHostApplicationBuilder AddDataServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<DigitalNotesDbContext>("digitalNotesDb");

        builder.Services.AddScoped<IEventStore, EventStore>();
        builder.Services.AddScoped<INoteRepository, NoteRepository>();
        builder.Services.AddScoped<INoteReadOnlyRepository, NoteReadOnlyRepository>();
        builder.Services.AddScoped(typeof(IEventDrivenRepository<>), typeof(EventDrivenRepository<>));

        builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
        builder.Services.AddScoped<IEventHandler<IDomainEvent>, NoteReadOnlyEventHandler>();

        return builder;
    }
}