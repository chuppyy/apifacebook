using ITC.Domain.Models.Itphonui;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Itphonui;

public class ProjectConfigCrudManagerMapping : IEntityTypeConfiguration<ProjectConfigCrudManager>
{
#region IEntityTypeConfiguration<ProjectConfigCrudManager> Members

    public void Configure(EntityTypeBuilder<ProjectConfigCrudManager> builder)
    {
        builder.ToTable("ProjectConfigCrudManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.ProjectManagerId).HasMaxLength(40);
    }

#endregion
}