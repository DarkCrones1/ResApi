using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasOne(d => d.BranchStore).WithMany(p => p.Payment)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Payment_BranchStore");

        builder.HasOne(d => d.CashRegister).WithMany(p => p.Payment)
            .HasForeignKey(d => d.CashRegisterId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Payment_BoxCash");

        builder.HasOne(d => d.Cashier).WithMany(p => p.Payment)
            .HasForeignKey(d => d.CashierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Payment_Employee");

        builder.HasOne(d => d.BranchStore).WithMany(p => p.Payment)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Payment_BranchStore");
    }
}