using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Admin')");

            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Cart)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Customer");

            builder.HasOne(d => d.BranchStore)
                .WithMany(p => p.Cart)
                .HasForeignKey(d => d.BranchStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_BranchStore");

            // builder.HasMany(d => d.Drink).WithMany(p => p.Cart)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "CartDrink",
            //         r => r.HasOne<Drink>().WithMany()
            //         .HasForeignKey("DrinkId")
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_CartDrink_Drink"),
            //         l => l.HasOne<Cart>().WithMany()
            //             .HasForeignKey("CartId")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("FK_CartDrink_Cart"),
            //         j =>
            //         {
            //             j.HasKey("CartId", "DrinkId");
            //         }
            //     );

            // builder.HasMany(d => d.Food).WithMany(p => p.Cart)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "CartFood",
            //         r => r.HasOne<Food>().WithMany()
            //         .HasForeignKey("FoodId")
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_CartFood_Food"),
            //         l => l.HasOne<Cart>().WithMany()
            //             .HasForeignKey("CartId")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("FK_CartFood_Cart"),
            //         j =>
            //         {
            //             j.HasKey("CartId", "DrinkId");
            //         }
            //     );
        }
    }
}
