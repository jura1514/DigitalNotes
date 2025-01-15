using DigitalNotes.API.Endpoints;
using DigitalNotes.Application;
using DigitalNotes.Infrastructure;
using DigitalNotes.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDataServices();

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

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();
app.MapNoteEndpoints();

app.Run();