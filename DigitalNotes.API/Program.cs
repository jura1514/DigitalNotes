using DigitalNotes.API.Endpoints;
using DigitalNotes.Application;
using DigitalNotes.Infrastructure;
using DigitalNotes.ServiceDefaults;
using Microsoft.OpenApi.Models;

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

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT token for authorization header",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "jwt"
    });
});
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

if (app.Environment.IsDevelopment())
{
    app.MapNoteEndpoints().AllowAnonymous();
}
else
{
    app.MapNoteEndpoints();
}

app.Run();