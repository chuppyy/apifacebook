#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ModuleMap : IEntityTypeConfiguration<Module>
{
#region IEntityTypeConfiguration<Module> Members

    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("AspNetModules");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.Name)
               .HasColumnName("Name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(s => s.Identity)
               .HasColumnName("Identity")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(s => s.Description)
               .HasColumnName("Description");

        builder.Property(s => s.CreateDate)
               .HasColumnName("CreateDate")
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(s => s.IsActivation)
               .HasColumnName("IsActivation")
               .IsRequired();

        //builder.Property(s => s.RoleId)
        //    .HasColumnName("RoleId")
        //    .HasMaxLength(450)
        //    .IsRequired();


        builder.Property(s => s.IsDeleted)
               .HasColumnName("IsDeleted")
               .IsRequired();

        builder.Property(s => s.ModuleGroupId)
               .HasColumnName("ModuleGroupId");

        var mlnavigation = builder.Metadata
                                  .FindNavigation(nameof(Module.ModuleRoles));
        mlnavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

#endregion
}