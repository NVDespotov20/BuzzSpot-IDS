using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class ApplicationLogConfiguration
    : IEntityTypeConfiguration<ApplicationLog>
{
    public void Configure(
        EntityTypeBuilder<ApplicationLog> builder)
    {
        builder.ToTable("application_logs");

        builder.Property(x => x.Level)
            .HasConversion<short>();

        builder.Property(x => x.Message)
            .IsRequired();

        builder.Property(x => x.PropertiesJson)
            .HasColumnType("jsonb");
    }
}