using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class NotificationUserManagerMapping : IEntityTypeConfiguration<NotificationUserManager>
{
    public void Configure(EntityTypeBuilder<NotificationUserManager> builder)
    {
        builder.ToTable("NotificationUserManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NotificationManagerId).HasMaxLength(40);
        builder.Property(x => x.StaffId).HasMaxLength(40);
    }
}