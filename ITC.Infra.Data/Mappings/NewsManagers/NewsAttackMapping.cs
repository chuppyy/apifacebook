using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsAttackMapping : IEntityTypeConfiguration<NewsAttack>
{
#region IEntityTypeConfiguration<NewsAttack> Members

    public void Configure(EntityTypeBuilder<NewsAttack> builder)
    {
        builder.ToTable("NewsAttacks");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NewsContentId).HasMaxLength(40);
        builder.Property(x => x.FileId).HasMaxLength(40);
    }

#endregion
}