using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsConfigMapping : IEntityTypeConfiguration<NewsConfig>
{
#region IEntityTypeConfiguration<NewsConfig> Members

    public void Configure(EntityTypeBuilder<NewsConfig> builder)
    {
        builder.ToTable("NewsConfigs");
        builder.Property(x => x.Id).HasColumnName("Id").HasMaxLength(40);
        builder.Property(x => x.TokenGit).HasMaxLength(250);
        builder.Property(x => x.OwnerGit).HasMaxLength(250);
        builder.Property(x => x.ProjectDefaultGit).HasMaxLength(250);
        builder.Property(x => x.TeamId).HasMaxLength(250);
        builder.Property(x => x.TokenVercel).HasMaxLength(250);
        builder.Property(x => x.Host).HasMaxLength(250);
    }
#endregion
}