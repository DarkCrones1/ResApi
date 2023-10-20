using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class PayBoxConfiguration : IEntityTypeConfiguration<PayBox>
{
    public void Configure(EntityTypeBuilder<PayBox> builder)
    {
        builder.HasOne(d => d.BoxCash).WithMany(p => p.PayBox)
            .HasForeignKey(d => d.BoxCashId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PayBox_BoxCash");

        builder.HasOne(d => d.BranchStore).WithMany(p => p.PayBox)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PayBox_BranchStore");

        builder.HasOne(d => d.Cashier).WithMany(p => p.PayBox)
            .HasForeignKey(d => d.CashierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PayBox_UserAccount");
    }
}