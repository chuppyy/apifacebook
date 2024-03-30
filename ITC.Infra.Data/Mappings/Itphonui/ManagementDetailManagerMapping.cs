using ITC.Domain.Models.Itphonui;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Itphonui;

public class ManagementDetailManagerMapping : IEntityTypeConfiguration<ManagementDetailManager>
{
#region IEntityTypeConfiguration<ManagementDetailManager> Members

    public void Configure(EntityTypeBuilder<ManagementDetailManager> builder)
    {
        builder.ToTable("ManagementDetailManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.ManagementManagerId).HasMaxLength(40);
    }

#endregion
}