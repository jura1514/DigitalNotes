using DigitalNotes.Domain.Common;
using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Hosting;

namespace DigitalNotes.Infrastructure;

public static class InfrastructureRegistration
{
    public static IHostApplicationBuilder AddDataServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<DigitalNotesDbContext>("digitalNotesDb");
        
        builder.Services.AddScoped<IEventStore, EventStore>();
        builder.Services.AddScoped<INotesRepository, NotesRepository>();
        builder.Services.AddScoped(typeof(IEventDrivenRepository<>), typeof(EventDrivenRepository<>));

        return builder;
    }
}