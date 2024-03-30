using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class PartnerManagerMapping : IEntityTypeConfiguration<PartnerManager>
{
#region IEntityTypeConfiguration<PartnerManager> Members

    public void Configure(EntityTypeBuilder<PartnerManager> builder)
    {
        builder.ToTable("PartnerManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Content).HasMaxLength(500);
    }

#endregion
}