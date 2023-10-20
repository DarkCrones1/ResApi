using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasOne(d => d.BranchStore).WithMany(p => p.Reservation)
            .HasForeignKey(d => d.BranchStoreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_BranchStore");

        builder.HasOne(d => d.Customer).WithMany(p => p.Reservation)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_Customer");

        builder.HasOne(d => d.Manager).WithMany(p => p.Reservation)
            .HasForeignKey(d => d.ManagerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_Employee");
    }
}