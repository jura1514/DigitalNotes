using DigitalNotes.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalNotes.Infrastructure.Data.Configurations;

internal class OutboxConfiguration : IEntityTypeConfiguration<Outbox>
{
    public void Configure(EntityTypeBuilder<Outbox> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.EventData).HasColumnType("jsonb");
        builder.Property(e => e.EventType).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
    }
}