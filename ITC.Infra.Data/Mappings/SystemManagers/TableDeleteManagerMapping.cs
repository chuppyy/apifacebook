using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class TableDeleteManagerMapping : IEntityTypeConfiguration<TableDeleteManager>
{
    public void Configure(EntityTypeBuilder<TableDeleteManager> builder)
    {
        builder.ToTable("TableDeleteManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(250);
    }
}