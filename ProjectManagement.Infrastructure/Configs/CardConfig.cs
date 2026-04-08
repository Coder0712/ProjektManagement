using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Boards;
using ProjectManagement.Domain.Cards;

namespace ProjectManagement.Infrastructure.Configs
{
    internal class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Card");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired();

            builder.Property(c => c.Description);

            builder.Property(c => c.Effort);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.Position)
                .IsRequired();

            builder.Property(g => g.CreatedAt)
                .IsRequired();

            builder.Property(g => g.LastModifiedAt)
                .IsRequired();
        }
    }
}
