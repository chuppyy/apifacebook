using ITC.Domain.Models.Itphonui;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Itphonui;

public class ProjectManagerMapping : IEntityTypeConfiguration<ProjectManager>
{
#region IEntityTypeConfiguration<ProjectManager> Members

    public void Configure(EntityTypeBuilder<ProjectManager> builder)
    {
        builder.ToTable("ProjectManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.HostName).HasMaxLength(500);
    }

#endregion
}