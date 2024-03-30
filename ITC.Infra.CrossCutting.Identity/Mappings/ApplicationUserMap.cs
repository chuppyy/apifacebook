#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
{
#region IEntityTypeConfiguration<ApplicationUser> Members

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(c => c.FullName)
               .HasColumnName("FullName")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(c => c.UserTypeId)
               .HasColumnName("UserTypeId")
               .IsRequired();

        builder.Property(c => c.IsDeleted)
               .HasColumnName("IsDeleted")
               .IsRequired();

        builder.Property(c => c.IsSuperAdmin)
               .HasColumnName("IsSuperAdmin")
               .IsRequired();

        builder.Property(c => c.UserTypeId)
               .HasColumnName("UserTypeId")
               .IsRequired();

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

        builder.Property(c => c.Avatar)
               .HasMaxLength(100);

        builder.Property(c => c.Address)
               .HasMaxLength(100);

        builder.Property(c => c.Fax)
               .HasMaxLength(20);

        builder.Property(c => c.Website)
               .HasMaxLength(100);


        builder.Property(x => x.ManagementUnitId)
               .HasColumnName("ManagementUnitId").HasMaxLength(40);

        builder.Property(x => x.PortalId)
               .HasColumnName("PortalId");
    }

#endregion
}