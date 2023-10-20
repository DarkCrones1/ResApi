using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Ignore(x => x.FullName);

        builder.Property(e => e.Birthday).HasColumnType("date");
        builder.Property(e => e.CellPhone).HasMaxLength(20);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Curp)
            .HasMaxLength(20)
            .HasColumnName("CURP");
        builder.Property(e => e.Email).HasMaxLength(150);
        builder.Property(e => e.FirstName).HasMaxLength(200);
        builder.Property(e => e.Ine)
            .HasMaxLength(20)
            .HasColumnName("INE");
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.LastName).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(150);
        builder.Property(e => e.Phone).HasMaxLength(20);

        builder.HasOne(d => d.InitialBranchStore).WithMany(p => p.Employee)
            .HasForeignKey(d => d.InitialBranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Employee_BranchStore");

        builder.HasOne(d => d.Job).WithMany(p => p.Employee)
            .HasForeignKey(d => d.JobId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Employee_Job");

        builder.HasMany(d => d.Address).WithMany(p => p.Employee)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeAddress",
                r => r.HasOne<Address>().WithMany()
                    .HasForeignKey("AddressId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAddress_Address"),
                l => l.HasOne<Employee>().WithMany()
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAddress_Employee"),
                j =>
                {
                    j.HasKey("EmployeeId", "AddressId");
                });
    }
}