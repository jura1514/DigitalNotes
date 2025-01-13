var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres")
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var digitalNotesDb = postgres.AddDatabase("digitalNotesDb");

builder.AddProject<Projects.DigitalNotes_MigrationService>("DigitalNotes-MigrationService")
    .WithReference(digitalNotesDb)
    .WaitFor(digitalNotesDb);

var digitalNotes = builder.AddProject<Projects.DigitalNotes_API>("DigitalNotes-API")
    .WithReference(digitalNotesDb)
    .WaitFor(digitalNotesDb);

// in case new service is added and needs to discover digital notes API, change and uncomment line below.
// builder.AddProject<Projects.SomeProject>("NewService").WithExternalHttpEndpoints().WithReference(digitalNotes);

builder.Build().Run();