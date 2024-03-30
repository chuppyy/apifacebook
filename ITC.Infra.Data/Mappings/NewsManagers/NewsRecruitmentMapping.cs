using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsRecruitmentMapping : IEntityTypeConfiguration<NewsRecruitment>
{
#region IEntityTypeConfiguration<NewsRecruitment> Members

    public void Configure(EntityTypeBuilder<NewsRecruitment> builder)
    {
        builder.ToTable("NewsRecruitments");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
        builder.Property(x => x.Summary).HasMaxLength(300);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.ViewEye).HasDefaultValue(0);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
    }

#endregion
}