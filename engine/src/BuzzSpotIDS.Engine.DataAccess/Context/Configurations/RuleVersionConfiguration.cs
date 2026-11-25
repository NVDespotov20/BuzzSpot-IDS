using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class RuleVersionConfiguration
    : IEntityTypeConfiguration<RuleVersion>
{
    public void Configure(
        EntityTypeBuilder<RuleVersion> builder)
    {
        builder.ToTable("rule_versions", table =>
        {
            table.HasCheckConstraint(
                "ck_rule_version_positive",
                "\"Version\" > 0");

            table.HasCheckConstraint(
                "ck_rule_severity",
                "\"Severity\" BETWEEN 1 AND 4");
        });

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Rule)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.RuleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
            {
                x.RuleId,
                x.Version
            })
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Severity)
            .HasConversion<short>();

        builder.Property(x => x.DefinitionJson)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}