using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class QueuedEventConfiguration
    : IEntityTypeConfiguration<QueuedEvent>
{
    public void Configure(
        EntityTypeBuilder<QueuedEvent> builder)
    {
        builder.ToTable("queued_events", table =>
        {
            table.HasCheckConstraint(
                "ck_queue_attempt_count",
                "\"AttemptCount\" >= 0");

            table.HasCheckConstraint(
                "ck_queue_lease",
                """
                (
                    "LockedBy" IS NULL
                    AND "LockedUntil" IS NULL
                    AND "LeaseToken" IS NULL
                )
                OR
                (
                    "LockedBy" IS NOT NULL
                    AND "LockedUntil" IS NOT NULL
                    AND "LeaseToken" IS NOT NULL
                )
                """);
        });

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Event)
            .WithOne(x => x.QueuedEvent)
            .HasForeignKey<QueuedEvent>(x => x.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.EventId)
            .IsUnique();

        builder.HasIndex(x => new
            {
                x.AvailableAt,
                x.EnqueuedAt
            })
            .HasFilter("\"DeadLetteredAt\" IS NULL");

        builder.Property(x => x.LockedBy)
            .HasMaxLength(200);
    }
}