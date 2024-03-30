using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsGithubMapping : IEntityTypeConfiguration<NewsGithub>
{
#region IEntityTypeConfiguration<NewsGithub> Members

    public void Configure(EntityTypeBuilder<NewsGithub> builder)
    {
        builder.ToTable("NewsGithubs");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Code).HasMaxLength(50);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.CreatedBy).HasMaxLength(40);
        builder.Property(x => x.ModifiedBy).HasMaxLength(40);
    }

#endregion
}