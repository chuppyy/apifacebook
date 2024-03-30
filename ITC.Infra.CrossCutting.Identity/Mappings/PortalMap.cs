#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class PortalMap : IEntityTypeConfiguration<Portal>
{
#region IEntityTypeConfiguration<Portal> Members

    public void Configure(EntityTypeBuilder<Portal> builder)
    {
        builder.ToTable("AspNetPortals");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
               .HasColumnName("Name")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(s => s.Identity)
               .HasColumnName("Identity")
               .HasMaxLength(450)
               .IsRequired();

        builder.Property(c => c.CreateDate)
               .HasColumnName("CreateDate")
               .HasColumnType("datetime")
               .HasDefaultValueSql("getutcdate()")
               .IsRequired();

        builder.Property(s => s.IsDeleted)
               .HasColumnName("IsDeleted")
               .IsRequired();

        builder.Property(s => s.IsDepartmentOfEducation)
               .HasColumnName("IsDepartmentOfEducation");
    }

#endregion
}