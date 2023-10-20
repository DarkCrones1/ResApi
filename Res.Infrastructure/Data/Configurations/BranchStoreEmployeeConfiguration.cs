using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class BranchStoreEmployeeConfiguration : IEntityTypeConfiguration<BranchStoreEmployee>
{
    public void Configure(EntityTypeBuilder<BranchStoreEmployee> builder)
    {
        builder.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.BranchStore).WithMany(p => p.BranchStoreEmployee)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BranchStoreEmployee_BranchStore");

        builder.HasOne(d => d.Job).WithMany(p => p.BranchStoreEmployee)
            .HasForeignKey(d => d.JobId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BranchStoreEmployee_BranchStoreEmployee");
    }
}