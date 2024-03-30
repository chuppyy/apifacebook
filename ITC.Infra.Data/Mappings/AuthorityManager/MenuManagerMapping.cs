using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class MenuManagerMapping : IEntityTypeConfiguration<MenuManager>
{
#region IEntityTypeConfiguration<Position> Members

    public void Configure(EntityTypeBuilder<MenuManager> builder)
    {
        builder.ToTable("MenuManager");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Code).HasMaxLength(150);
        builder.Property(x => x.Label).HasMaxLength(100);
        builder.Property(x => x.ManagerICon).HasMaxLength(100);
    }

#endregion
}