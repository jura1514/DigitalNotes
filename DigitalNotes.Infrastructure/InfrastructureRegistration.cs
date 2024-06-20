using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DigitalNotes.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSettingsSection = configuration.GetSection(DataSettings.Section);
        var dataSettings = dataSettingsSection.Get<DataSettings>();

        if (dataSettings is null)
            throw new ArgumentNullException(DataSettings.Section, "Data setting are not set.");

        services.Configure<DataSettings>(dataSettingsSection);
        services.AddSingleton(registeredServices =>
            registeredServices.GetRequiredService<IOptions<DataSettings>>().Value);

        services.AddScoped<INotesRepository, NotesRepository>();

        services.AddDbContext<DigitalNotesDbContext>(options =>
        {
            if (dataSettings.UseInMemory)
            {
                options.UseInMemoryDatabase("NotesDatabase");
            }
            else
            {
                options.UseNpgsql(dataSettings.ConnectionString);
            }
        });

        services.AddHostedService<DigitalNotesDbContextMigration>();

        return services;
    }
}