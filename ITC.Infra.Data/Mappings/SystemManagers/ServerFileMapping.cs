using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class ServerFileMapping : IEntityTypeConfiguration<ServerFile>
{
#region IEntityTypeConfiguration<ServerFile> Members

    public void Configure(EntityTypeBuilder<ServerFile> builder)
    {
        builder.ToTable("ServerFiles");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.FileExtension).HasMaxLength(150);
        builder.Property(x => x.FileName).HasMaxLength(500);
        builder.Property(x => x.FileType).HasMaxLength(40);
        builder.Property(x => x.Folder).HasMaxLength(40).IsRequired(false);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.CloudId).HasMaxLength(40).IsRequired(false);
        builder.Property(x => x.CloudFolder).HasMaxLength(500).IsRequired(false);
        builder.Property(x => x.ManagementId).HasMaxLength(40).IsRequired(false);
    }

#endregion
}