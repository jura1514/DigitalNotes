using DigitalNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotes.Infrastructure.Data;

public class DigitalNotesDbContext : DbContext
{
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteView> NotesView => Set<NoteView>();

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