using ITC.Domain.Models.Itphonui;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Itphonui;

public class ManagementManagerMapping : IEntityTypeConfiguration<ManagementManager>
{
#region IEntityTypeConfiguration<ManagementManager> Members

    public void Configure(EntityTypeBuilder<ManagementManager> builder)
    {
        builder.ToTable("ManagementManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.Symbol).HasMaxLength(20);
        builder.Property(x => x.AccountDefault).HasMaxLength(50);
        //builder.Property(x => x.Province).HasMaxLength(1000);
    }

#endregion
}