using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class UserOnProjectMapping : IEntityTypeConfiguration<UserOnProject>
{

    public void Configure(EntityTypeBuilder<UserOnProject> builder)
    {
        builder.ToTable("users_on_projects");

        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ProjectId).HasColumnName("project_id");
        builder.Property(x => x.Role).HasColumnName("role");

        builder.HasOne(x => x.User).WithMany(x=>x.Projects).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
        builder.HasOne(x => x.Project).WithMany(x=>x.Members).HasForeignKey(x => x.ProjectId).HasPrincipalKey(x => x.Id);
        builder.Property(x => x.Role).IsRequired();
    }
}
