using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings; 

public class TaskMapping : IEntityTypeConfiguration<Domain.Models.Task>
{

    public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
    {
        builder.ToTable("tasks");
        
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(512).HasColumnName("title");

        builder.Property(x=>x.Description).IsRequired().HasColumnType("text").HasColumnName("description");

        builder.Property(x => x.StartDate)
            .HasColumnName("start_date");

        builder.Property(x => x.DeadLine)
            .HasColumnName("deadline");

        builder.Property(x => x.Priority)
            .HasColumnName("priority");

            
    }
}
