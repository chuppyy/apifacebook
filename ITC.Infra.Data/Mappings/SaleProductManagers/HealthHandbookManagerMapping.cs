using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class HealthHandbookManagerMapping : IEntityTypeConfiguration<HealthHandbookManager>
{
#region IEntityTypeConfiguration<HealthHandbookManager> Members

    public void Configure(EntityTypeBuilder<HealthHandbookManager> builder)
    {
        builder.ToTable("HealthHandbookManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
        builder.Property(x => x.Summary).HasMaxLength(300);
        builder.Property(x => x.Author).HasMaxLength(200);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.ViewEye).HasDefaultValue(0);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
    }

#endregion
}