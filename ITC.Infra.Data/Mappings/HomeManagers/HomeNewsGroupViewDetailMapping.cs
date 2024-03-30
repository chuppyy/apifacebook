using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeNewsGroupViewDetailMapping : IEntityTypeConfiguration<HomeNewsGroupViewDetail>
{
#region IEntityTypeConfiguration<HomeNewsGroupViewDetail> Members

    public void Configure(EntityTypeBuilder<HomeNewsGroupViewDetail> builder)
    {
        builder.ToTable("HomeNewsGroupViewDetails");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NewsGroupId).HasMaxLength(40);
        builder.Property(x => x.HomeNewsGroupViewId).HasMaxLength(40);
    }

#endregion
}