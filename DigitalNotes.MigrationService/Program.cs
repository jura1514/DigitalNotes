using DigitalNotes.Infrastructure.Data;
using DigitalNotes.MigrationService;
using DigitalNotes.ServiceDefaults;
using DigitalNotes.Identity.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<DigitalNotesDbContext>("digitalNotesDb");
builder.AddNpgsqlDbContext<DigitalNotesIdentityDbContext>("identityDb");

var host = builder.Build();
host.Run();