using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Ignore(x => x.FullName);
        builder.HasKey(e => e.Id).HasName("PK__Customer__3214EC076D7D9088");


        builder.Property(e => e.CellPhone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Email)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.FirstName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.LastName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.MiddleName)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.HasOne(d => d.CreatedAtBranchStore).WithMany(p => p.Customer)
            .HasForeignKey(d => d.CreatedAtBranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Customer_Customer");

        builder.HasOne(d => d.CustomerType).WithMany(p => p.Customer)
            .HasForeignKey(d => d.CustomerTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Customer_CustomerType");
    }
}