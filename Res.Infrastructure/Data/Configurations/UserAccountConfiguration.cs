using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        builder.Property(e => e.Password).HasMaxLength(100);
        builder.Property(e => e.UserName).HasMaxLength(150);

        builder.HasMany(d => d.Employee).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountEmployee",
                r => r.HasOne<Employee>().WithMany()
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountEmployee_Employee"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountEmployee_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "EmployeeId");
                });

        builder.HasMany(d => d.Customer).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountCustomer",
                r => r.HasOne<Customer>().WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountCustomer_Customer"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_UserAccountCustomer_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "CustomerId");
                });

        builder.HasMany(d => d.Rol).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserRol",
                r => r.HasOne<Rol>().WithMany()
                    .HasForeignKey("RolId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRol_Rol"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRol_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "RolId");
                });
    }
}