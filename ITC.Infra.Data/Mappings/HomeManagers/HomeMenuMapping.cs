using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeMenuMapping : IEntityTypeConfiguration<HomeMenu>
{
#region IEntityTypeConfiguration<HomeMenu> Members

    public void Configure(EntityTypeBuilder<HomeMenu> builder)
    {
        builder.ToTable("HomeMenus");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
    }

#endregion
}