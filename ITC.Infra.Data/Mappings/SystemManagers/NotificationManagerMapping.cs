using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class NotificationManagerMapping : IEntityTypeConfiguration<NotificationManager>
{
    public void Configure(EntityTypeBuilder<NotificationManager> builder)
    {
        builder.ToTable("NotificationManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.FileAttackId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.ManagementId).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
    }
}