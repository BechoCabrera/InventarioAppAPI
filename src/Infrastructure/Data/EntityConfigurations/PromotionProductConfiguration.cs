using InventarioBackend.src.Core.Domain.Promotions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventarioBackend.src.Infrastructure.Data.EntityConfigurations
{
    public class PromotionProductConfiguration : IEntityTypeConfiguration<PromotionProduct>
    {
        public void Configure(EntityTypeBuilder<PromotionProduct> builder)
        {
            builder.ToTable("PromotionProducts");

            builder.HasKey(pp => new { pp.PromotionId, pp.ProductId });

            builder.HasOne(pp => pp.Promotion)
                .WithMany(p => p.PromotionProducts)
                .HasForeignKey(pp => pp.PromotionId);

            // Opcional: si quieres relación directa con Product
            // builder.HasOne<Product>()
            //     .WithMany()
            //     .HasForeignKey(pp => pp.ProductId);
        }
    }
}