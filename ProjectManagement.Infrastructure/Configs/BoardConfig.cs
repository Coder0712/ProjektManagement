using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Boards;

namespace ProjectManagement.Infrastructure.Configs
{
    public class BoardConfig : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.ToTable("Board");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.LastModifiedAt)
                .IsRequired();

            builder.HasMany(b => b.Groups)
                .WithOne(c => c.Board)
                .HasForeignKey(c => c.BoardId);
        }
    }
}
