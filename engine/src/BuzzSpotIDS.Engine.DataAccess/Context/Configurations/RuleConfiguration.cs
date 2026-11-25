using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class RuleConfiguration
    : IEntityTypeConfiguration<Rule>
{
    public void Configure(
        EntityTypeBuilder<Rule> builder)
    {
        builder.ToTable("rules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}