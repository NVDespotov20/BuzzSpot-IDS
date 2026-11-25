using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class EventConfiguration
    : IEntityTypeConfiguration<Event>
{
    public void Configure(
        EntityTypeBuilder<Event> builder)
    {
        builder.UseTptMappingStrategy();

        builder.ToTable("events");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Source)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.SourceId,
            x.OccurredAt
        });

        builder.HasIndex(x => x.OccurredAt);

        builder.Property(x => x.TraceId)
            .HasMaxLength(32);

        builder.Property(x => x.SpanId)
            .HasMaxLength(16);
    }
}