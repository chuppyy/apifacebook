using ITC.Domain.Models.StudyManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.StudyManagers;

public class SubjectTypeManagerMapping : IEntityTypeConfiguration<SubjectTypeManager>
{
#region IEntityTypeConfiguration<SubjectTypeManager> Members

    public void Configure(EntityTypeBuilder<SubjectTypeManager> builder)
    {
        builder.ToTable("SubjectTypeManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
    }

#endregion
}