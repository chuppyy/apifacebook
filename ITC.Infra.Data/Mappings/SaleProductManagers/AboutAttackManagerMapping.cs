using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class AboutAttackManagerMapping : IEntityTypeConfiguration<AboutAttackManager>
{
#region IEntityTypeConfiguration<AboutAttackManager> Members

    public void Configure(EntityTypeBuilder<AboutAttackManager> builder)
    {
        builder.ToTable("AboutAttackManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.AboutManagerId).HasMaxLength(40);
    }

#endregion
}