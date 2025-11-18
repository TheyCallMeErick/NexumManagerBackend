using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings; 

public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{

    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.Property(e => e.Token)
              .HasColumnName("token")
              .IsRequired()
              .HasMaxLength(500);

        builder.Property(e => e.ExpiresAt)
              .HasColumnName("expires_at")
              .IsRequired();

        builder.Property(e => e.IsRevoked)
              .HasColumnName("is_revoked")
              .IsRequired();

        builder.Property(e => e.RevokedAt)
              .HasColumnName("revoked_at")
              .IsRequired(false);

        builder.Property(e => e.CreatedByIp)
              .HasColumnName("created_by_ip")
              .HasMaxLength(45);

        builder.Property(e => e.DeviceInfo)
              .HasColumnName("device_info")
              .HasMaxLength(200);
    }

}