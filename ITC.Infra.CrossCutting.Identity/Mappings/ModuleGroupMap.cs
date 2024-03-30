#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ModuleGroupMap : IEntityTypeConfiguration<ModuleGroup>
{
#region IEntityTypeConfiguration<ModuleGroup> Members

    public void Configure(EntityTypeBuilder<ModuleGroup> builder)
    {
        builder.ToTable("AspNetModuleGroups");

        builder.Property(c => c.Id)
               .HasColumnName("Id");
    }

#endregion
}