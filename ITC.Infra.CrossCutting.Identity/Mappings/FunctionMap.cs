#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class FunctionMap : IEntityTypeConfiguration<Function>
{
#region IEntityTypeConfiguration<Function> Members

    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.ToTable("AspNetFunctions");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.Name)
               .HasColumnName("Name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(s => s.Weight)
               .HasColumnName("Weight")
               .IsRequired();

        builder.Property(s => s.Description)
               .HasColumnName("Description");

        builder.Property(s => s.CreateDate)
               .HasColumnName("CreateDate")
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(s => s.ModuleId)
               .HasColumnName("ModuleId")
               .IsRequired();

        builder.Property(s => s.IsActivation)
               .HasColumnName("IsActivation")
               .IsRequired();

        var fnavigation = builder.Metadata
                                 .FindNavigation(nameof(Function.FunctionRoles));
        fnavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

#endregion
}