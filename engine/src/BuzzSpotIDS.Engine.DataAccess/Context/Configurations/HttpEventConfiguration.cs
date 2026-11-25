using BuzzSpotIDS.Engine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuzzSpotIDS.Engine.DataAccess.Context.Configurations;

public class HttpEventConfiguration
    : IEntityTypeConfiguration<HttpEvent>
{
    public void Configure(
        EntityTypeBuilder<HttpEvent> builder)
    {
        builder.ToTable("http_events");

        builder.Property(x => x.Method)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Scheme)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.Host)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.ClientIp)
            .HasColumnType("inet");

        builder.Property(x => x.RequestHeadersJson)
            .HasColumnType("jsonb");

        builder.Property(x => x.ResponseHeadersJson)
            .HasColumnType("jsonb");
    }
}