using InventarioBackend.src.Core.Domain.Promotions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventarioBackend.src.Infrastructure.Data.EntityConfigurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(p => p.PromotionId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Percentage)
                .HasPrecision(18, 2);

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.HasMany(p => p.PromotionProducts)
                .WithOne(pp => pp.Promotion)
                .HasForeignKey(pp => pp.PromotionId);
        }
    }
}