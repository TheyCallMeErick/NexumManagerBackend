using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings; 

public class ProjectMapping : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.Property(e => e.Title)
              .IsRequired()
              .HasColumnName("title")
              .HasMaxLength(255);

        builder.Property(e => e.Description)
              .IsRequired()
              .HasColumnName("description")
              .HasMaxLength(512);
    }

}
