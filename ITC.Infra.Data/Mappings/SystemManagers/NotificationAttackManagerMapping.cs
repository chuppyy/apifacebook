using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class NotificationAttackManagerMapping : IEntityTypeConfiguration<NotificationAttackManager>
{
    public void Configure(EntityTypeBuilder<NotificationAttackManager> builder)
    {
        builder.ToTable("NotificationAttackManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NotificationManagerId).HasMaxLength(40);
        builder.Property(x => x.FileId).HasMaxLength(40);
    }
}