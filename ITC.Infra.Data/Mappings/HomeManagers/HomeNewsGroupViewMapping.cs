using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeNewsGroupViewMapping : IEntityTypeConfiguration<HomeNewsGroupView>
{
#region IEntityTypeConfiguration<HomeNewsGroupView> Members

    public void Configure(EntityTypeBuilder<HomeNewsGroupView> builder)
    {
        builder.ToTable("HomeNewsGroupViews");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
    }

#endregion
}