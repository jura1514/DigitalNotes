using DigitalNotes.API.Endpoints;
using DigitalNotes.Application;
using DigitalNotes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000", "http://localhost:8080");
                
        if (builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            // To allow any port origin in localhost
            policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback);
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddApplicationServices();
builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors();

app.UseHttpsRedirection();

app.MapNoteEndpoints();

app.Run();