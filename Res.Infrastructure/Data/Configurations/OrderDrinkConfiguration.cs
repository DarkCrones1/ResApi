using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class OrderDrinkConfiguration : IEntityTypeConfiguration<OrderDrink>
{
    public void Configure(EntityTypeBuilder<OrderDrink> builder)
    {
        builder.HasOne(e => e.Drink).WithMany(p => p.OrderDrink)
        .HasForeignKey(d => d.DrinkId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_OrderDrink_Drink");

        builder.HasOne(e => e.Order).WithMany(p => p.OrderDrink)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_OrderDrink_Order");
    }
}