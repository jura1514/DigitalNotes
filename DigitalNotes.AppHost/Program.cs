var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres")
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithPgAdmin();

var digitalNotesDb = postgres.AddDatabase("digitalNotesDb");
var identityDb = postgres.AddDatabase("identityDb");

var identity = builder.AddProject<Projects.DigitalNotes_Identity>("DigitalNotes-Identity")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.AddProject<Projects.DigitalNotes_MigrationService>("DigitalNotes-MigrationService")
    .WithReference(digitalNotesDb)
    .WaitFor(digitalNotesDb)
    .WithReference(identityDb)
    .WaitFor(identityDb);

var digitalNotes = builder.AddProject<Projects.DigitalNotes_API>("DigitalNotes-API")
    .WithReference(digitalNotesDb)
    .WaitFor(digitalNotesDb)
    .WithExternalHttpEndpoints().WithReference(identity);

// in case new service is added and needs to discover digital notes API, change and uncomment line below.
// builder.AddProject<Projects.SomeProject>("NewService").WithExternalHttpEndpoints().WithReference(digitalNotes);

builder.Build().Run();