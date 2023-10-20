using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class OrderFoodConfiguration : IEntityTypeConfiguration<OrderFood>
{
    public void Configure(EntityTypeBuilder<OrderFood> builder)
    {
        builder.HasOne(e => e.Food).WithMany(p => p.OrderFood)
        .HasForeignKey(d => d.FoodId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_CartFood_Cart");

        builder.HasOne(e => e.Order).WithMany(p => p.OrderFood)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Food_Cart");
    }
}