#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class FunctionRoleMap : IEntityTypeConfiguration<FunctionRole>
{
#region IEntityTypeConfiguration<FunctionRole> Members

    public void Configure(EntityTypeBuilder<FunctionRole> builder)
    {
        builder.ToTable("AspNetFunctionRoles");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.FunctionId)
               .HasColumnName("FunctionId")
               .IsRequired();

        builder.Property(s => s.RoleId)
               .HasColumnName("RoleId")
               .HasMaxLength(450)
               .IsRequired();
    }

#endregion
}