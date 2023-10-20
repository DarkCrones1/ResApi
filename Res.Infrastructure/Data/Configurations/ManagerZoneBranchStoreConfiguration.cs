using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class ManagerZoneBranchStoreConfiguration : IEntityTypeConfiguration<ManagerZoneBranchStore>
{
    public void Configure(EntityTypeBuilder<ManagerZoneBranchStore> builder)
    {
        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.BranchStore).WithMany(p => p.ManagerZoneBranchStore)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ManagerZoneBranchStore_BranchStore");

        builder.HasOne(d => d.Employee).WithMany(p => p.ManagerZoneBranchStore)
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ManagerZoneBranchStore_Employee");
    }
}