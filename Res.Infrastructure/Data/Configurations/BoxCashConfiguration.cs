// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// using Res.Domain.Entities;

// namespace Res.Infrastructure.Data.Configurations;

// public class BoxCashConfiguration : IEntityTypeConfiguration<BoxCash>
// {
//     public void Configure(EntityTypeBuilder<BoxCash> builder)
//     {
//         builder.Property(e => e.CreatedBy)
//                 .HasMaxLength(20)
//                 .HasDefaultValueSql("(N'Admin')");
//         builder.Property(e => e.CreatedDate)
//             .HasDefaultValueSql("(getdate())")
//             .HasColumnType("datetime");
//         builder.Property(e => e.Description).HasMaxLength(150);
//         builder.Property(e => e.LastModifiedBy).HasMaxLength(20);
//         builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
//         builder.Property(e => e.Name).HasMaxLength(50);
//         builder.Property(e => e.SerialNumber).HasMaxLength(50);
//     }
// }