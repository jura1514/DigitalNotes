using DigitalNotes.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalNotes.Infrastructure.Data.Configurations;

internal class EventStoreConfiguration : IEntityTypeConfiguration<EventStoreEntity>
{
    public void Configure(EntityTypeBuilder<EventStoreEntity> builder)
    {
        builder.HasKey(e => e.EventId);
        builder.Property(e => e.EventData).HasColumnType("jsonb");
        builder.Property(e => e.EventType).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
    }
}
