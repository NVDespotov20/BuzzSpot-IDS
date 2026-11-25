using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class AlertConfiguration
    : IEntityTypeConfiguration<Alert>
{
    public void Configure(
        EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("alerts", table =>
        {
            table.HasCheckConstraint(
                "ck_alert_status",
                "\"Status\" BETWEEN 1 AND 4");
        });

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Event)
            .WithMany(x => x.Alerts)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RuleVersion)
            .WithMany(x => x.Alerts)
            .HasForeignKey(x => x.RuleVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
            {
                x.EventId,
                x.RuleVersionId
            })
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.Status,
            x.DetectedAt
        });

        builder.Property(x => x.Status)
            .HasConversion<short>();

        builder.Property(x => x.EvidenceJson)
            .HasColumnType("jsonb");
    }
}