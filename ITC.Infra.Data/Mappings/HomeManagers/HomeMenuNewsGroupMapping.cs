using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeMenuNewsGroupMapping : IEntityTypeConfiguration<HomeMenuNewsGroup>
{
#region IEntityTypeConfiguration<HomeMenuNewsGroup> Members

    public void Configure(EntityTypeBuilder<HomeMenuNewsGroup> builder)
    {
        builder.ToTable("HomeMenuNewsGroups");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NewsGroupId).HasMaxLength(40);
        builder.Property(x => x.HomeMenuId).HasMaxLength(40);
    }

#endregion
}