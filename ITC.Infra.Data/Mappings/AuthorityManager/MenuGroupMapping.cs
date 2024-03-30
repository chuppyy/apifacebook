using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class MenuGroupMapping : IEntityTypeConfiguration<MenuGroup>
{
#region IEntityTypeConfiguration<MenuGroup> Members

    public void Configure(EntityTypeBuilder<MenuGroup> builder)
    {
        builder.ToTable("MenuGroups");
        builder.Property(x => x.Id).HasColumnName("Id").HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
    }

#endregion
}