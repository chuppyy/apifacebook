using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsGroupTypeMapping : IEntityTypeConfiguration<NewsGroupType>
{
#region IEntityTypeConfiguration<NewsGroupType> Members

    public void Configure(EntityTypeBuilder<NewsGroupType> builder)
    {
        builder.ToTable("NewsGroupTypes");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.SecretKey).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.MetaTitle).HasMaxLength(250);
    }

#endregion
}