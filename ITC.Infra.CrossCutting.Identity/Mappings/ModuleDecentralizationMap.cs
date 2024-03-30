#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ModuleDecentralizationMap : IEntityTypeConfiguration<ModuleDecentralization>
{
#region IEntityTypeConfiguration<ModuleDecentralization> Members

    public void Configure(EntityTypeBuilder<ModuleDecentralization> builder)
    {
        builder.ToTable("AspNetModuleDecentralizations");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.ModuleId)
               .HasColumnName("ModuleId")
               .IsRequired();

        builder.Property(s => s.RoleId)
               .HasColumnName("RoleId")
               .HasMaxLength(450)
               .IsRequired();

        builder.Property(s => s.UserTypeId)
               .HasColumnName("UserTypeId")
               .IsRequired();
    }

#endregion
}