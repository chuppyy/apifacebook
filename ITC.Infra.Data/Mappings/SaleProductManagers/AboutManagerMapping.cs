using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class AboutManagerMapping : IEntityTypeConfiguration<AboutManager>
{
#region IEntityTypeConfiguration<AboutManager> Members

    public void Configure(EntityTypeBuilder<AboutManager> builder)
    {
        builder.ToTable("AboutManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Summary).HasMaxLength(500);
        builder.Property(x => x.MetaLink).HasMaxLength(500);
    }

#endregion
}