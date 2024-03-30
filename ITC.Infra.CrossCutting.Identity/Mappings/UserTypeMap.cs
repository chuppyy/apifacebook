#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class UserTypeMap : IEntityTypeConfiguration<UserType>
{
#region IEntityTypeConfiguration<UserType> Members

    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.ToTable("AspNetUserTypes");

        builder.Property(c => c.Id)
               .HasColumnName("Id");

        builder.Property(s => s.Name)
               .HasColumnName("Name")
               .IsRequired();

        builder.Property(s => s.Description)
               .HasColumnName("Description");

        builder.Property(s => s.RoleId)
               .HasColumnName("RoleId")
               .HasMaxLength(450)
               .IsRequired();

        builder.Property(s => s.IsSuperAdmin)
               .HasColumnName("IsSuperAdmin")
               .IsRequired();

        builder.Property(s => s.IsDefault)
               .HasColumnName("IsDefault")
               .IsRequired();

        builder.Property(s => s.ManagementUnitId)
               .HasColumnName("ManagementUnitId");

        builder.Property(s => s.IsDeleted)
               .HasColumnName("IsDeleted")
               .IsRequired();

        builder.Property(x => x.PortalId)
               .HasColumnName("PortalId");

        builder.OwnsOne(x => x.User,
                        o =>
                        {
                            o.Property(y => y.CreateBy)
                             .HasColumnName("CreateBy")
                             .IsRequired();

                            o.Property(y => y.ModifyBy)
                             .HasColumnName("ModifyBy")
                             .IsRequired();

                            o.Property(y => y.CreateDate)
                             .HasColumnName("CreateDate")
                             .HasColumnType("datetime")
                             .HasDefaultValueSql("getutcdate()")
                             .IsRequired();

                            o.Property(y => y.ModifyDate)
                             .HasColumnName("ModifyDate")
                             .HasColumnType("datetime")
                             .HasDefaultValueSql("getutcdate()")
                             .IsRequired();
                        });
    }

#endregion
}