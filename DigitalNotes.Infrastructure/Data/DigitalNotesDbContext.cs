using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Infrastructure.Data.Configurations;
using DigitalNotes.Infrastructure.Data.Entities;

namespace DigitalNotes.Infrastructure.Data;

public class DigitalNotesDbContext : DbContext
{
    public DbSet<NoteReadOnly> NotesReadOnly => Set<NoteReadOnly>();
    public DbSet<EventStoreEntity> EventStore => Set<EventStoreEntity>();

    public DigitalNotesDbContext()
    {
    }

    public DigitalNotesDbContext(DbContextOptions<DigitalNotesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new EventStoreConfiguration().Configure(modelBuilder.Entity<EventStoreEntity>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost:5432;Database=postgres;Username=postgres;Password=user");
        }
    }

    public Task MigrateAsync()
    {
        return Database.MigrateAsync();
    }
}