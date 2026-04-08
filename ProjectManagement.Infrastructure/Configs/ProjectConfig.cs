using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Projects;

namespace ProjectManagement.Infrastructure.Configs
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name);

            builder.Property(p => p.Description);

            builder.Property(p => p.Status);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.LastModifiedAt)
                .IsRequired();

            builder.HasOne(p => p.Board)
                .WithOne(b => b.Project)
                .HasForeignKey<Board>(b => b.ProjectId);
        }
    }
}
