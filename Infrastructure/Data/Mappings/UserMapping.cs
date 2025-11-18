using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings; 

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(e => e.Username)
              .HasColumnName("username")
              .HasMaxLength(50);

        builder.Property(e => e.Email)
              .HasColumnName("email")
              .HasMaxLength(100);

        builder.Property(e => e.Name)
              .HasColumnName("name")
            .HasMaxLength(255);

        builder.Property(e => e.PasswordHash)
              .HasColumnName("password_hash")
              .IsRequired();

        builder.Property(e => e.ProfilePictureFileName)
              .HasColumnName("profile_picture_filename")
              .HasMaxLength(1024);

        builder.HasMany(e => e.RefreshTokens)
               .WithOne(e => e.User)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}