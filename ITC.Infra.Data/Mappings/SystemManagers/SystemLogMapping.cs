using ITC.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class SystemLogMapping : IEntityTypeConfiguration<SystemLog>
{
    public void Configure(EntityTypeBuilder<SystemLog> builder)
    {
        builder.ToTable("SystemLogs");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.UserCreateId).HasMaxLength(40);
        builder.Property(x => x.UserCreateName).HasMaxLength(500);
        builder.Property(x => x.NameFile).HasMaxLength(500);
        builder.Property(x => x.DataId).HasMaxLength(40).IsRequired(false);
    }
}