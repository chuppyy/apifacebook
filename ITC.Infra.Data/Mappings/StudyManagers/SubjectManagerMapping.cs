using ITC.Domain.Models.StudyManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.StudyManagers;

public class SubjectManagerMapping : IEntityTypeConfiguration<SubjectManager>
{
#region IEntityTypeConfiguration<SubjectManager> Members

    public void Configure(EntityTypeBuilder<SubjectManager> builder)
    {
        builder.ToTable("SubjectManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
        builder.Property(x => x.SubjectTypeManagerId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.MetaTitle).HasMaxLength(250);
    }

#endregion
}