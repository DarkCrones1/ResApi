using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class CartDrinkConfiguration : IEntityTypeConfiguration<CartDrink>
{
    public void Configure(EntityTypeBuilder<CartDrink> builder)
    {       
        builder.HasOne(e => e.Cart).WithMany(p => p.CartDrink)
        .HasForeignKey(d => d.CartId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_CartDrink_Cart");

        builder.HasOne(e => e.Drink).WithMany(p => p.CartDrink)
        .HasForeignKey(d => d.DrinkId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_CartDrink_Drink");
    }
}