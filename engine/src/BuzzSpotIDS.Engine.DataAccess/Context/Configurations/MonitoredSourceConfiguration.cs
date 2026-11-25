using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class MonitoredSourceConfiguration
    : IEntityTypeConfiguration<MonitoredSource>
{
    public void Configure(
        EntityTypeBuilder<MonitoredSource> builder)
    {
        builder.ToTable("monitored_sources");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.BaseUrl)
            .HasMaxLength(2048);
    }
}
