using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class PermissionDefaultMapping : IEntityTypeConfiguration<PermissionDefault>
{
#region IEntityTypeConfiguration<PermissionDefault> Members

    public void Configure(EntityTypeBuilder<PermissionDefault> builder)
    {
        builder.ToTable("PermissionDefaults");
        builder.Property(x => x.Id).HasColumnName("Id").HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
    }

#endregion
}