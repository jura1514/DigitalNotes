using DigitalNotes.Domain.Entities;
using DigitalNotes.Infrastructure.Data.Configurations;
using DigitalNotes.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotes.Infrastructure.Data;

public class DigitalNotesDbContext : DbContext
{
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteView> NotesView => Set<NoteView>();
    public DbSet<EventStoreEntity> EventStore => Set<EventStoreEntity>();

    public DigitalNotesDbContext()
    {
    }

    public DigitalNotesDbContext(DbContextOptions<DigitalNotesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NoteView>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("view_notes");
        });

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