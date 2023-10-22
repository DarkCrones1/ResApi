using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data.Configurations;

public class ActiveUserAccountEmployeeConfiguration : IEntityTypeConfiguration<ActiveUserAccountEmployee>
{
    public void Configure(EntityTypeBuilder<ActiveUserAccountEmployee> builder)
    {
        builder
                .HasNoKey()
                .ToView("VW_ActiveUserAccountEmployee");

        builder.Property(e => e.CellPhone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(150);
        builder.Property(e => e.FirstName).HasMaxLength(200);
        builder.Property(e => e.JobName).HasMaxLength(50);
        builder.Property(e => e.LastName).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(150);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.UserName).HasMaxLength(150);

        // Configure id don't delete
        builder.Property(e => e.Id).HasColumnName("UserAccountId");
    }
}