using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class CartFoodConfiguration : IEntityTypeConfiguration<CartFood>
{
    public void Configure(EntityTypeBuilder<CartFood> builder)
    {
        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(e => e.Cart).WithMany(p => p.CartFood)
        .HasForeignKey(d => d.CartId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_CartFood_Cart");

        builder.HasOne(e => e.Food).WithMany(p => p.CartFood)
        .HasForeignKey(d => d.FoodId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_CartFood_Food");
    }
}