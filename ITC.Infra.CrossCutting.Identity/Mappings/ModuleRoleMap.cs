#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ModuleRoleMap : IEntityTypeConfiguration<ModuleRole>
{
#region IEntityTypeConfiguration<ModuleRole> Members

    public void Configure(EntityTypeBuilder<ModuleRole> builder)
    {
        builder.ToTable("AspNetModuleRoles");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.ModuleId)
               .HasColumnName("ModuleId")
               .IsRequired();

        builder.Property(s => s.RoleId)
               .HasColumnName("RoleId")
               .HasMaxLength(450)
               .IsRequired();
    }

#endregion
}