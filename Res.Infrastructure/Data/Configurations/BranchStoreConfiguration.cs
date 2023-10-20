using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class BranchStoreConfiguration : IEntityTypeConfiguration<BranchStore>
{
    public void Configure(EntityTypeBuilder<BranchStore> builder)
    {
        builder.Property(e => e.Code)
            .HasMaxLength(10)
            .HasDefaultValueSql("(N'SUC')");
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Email).HasMaxLength(250);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(250);
        builder.Property(e => e.Phone).HasMaxLength(20);

        builder.HasMany(d => d.Address).WithMany(p => p.BranchStore)
            .UsingEntity<Dictionary<string, object>>(
                "BranchStoreAddress",
                r => r.HasOne<Address>().WithMany()
                    .HasForeignKey("AddressId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchStoreAddress_Address"),
                l => l.HasOne<BranchStore>().WithMany()
                    .HasForeignKey("BranchStoreId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchStoreAddress_BranchStore"),
                j =>
                {
                    j.HasKey("BranchStoreId", "AddressId");
                });

        builder.HasMany(d => d.UserAccount).WithMany(p => p.BranchStore)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountBranchStore",
                r => r.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountBranchStore_UserAccount"),
                l => l.HasOne<BranchStore>().WithMany()
                    .HasForeignKey("BranchStoreId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountBranchStore_BranchStore"),
                j =>
                {
                    j.HasKey("BranchStoreId", "UserAccountId");
                });
    }
}