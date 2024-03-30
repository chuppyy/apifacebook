#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class FunctionDecentralizationMap : IEntityTypeConfiguration<FunctionDecentralization>
{
#region IEntityTypeConfiguration<FunctionDecentralization> Members

    public void Configure(EntityTypeBuilder<FunctionDecentralization> builder)
    {
        builder.ToTable("AspNetFunctionDecentralizations");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.FunctionId)
               .HasColumnName("FunctionId")
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