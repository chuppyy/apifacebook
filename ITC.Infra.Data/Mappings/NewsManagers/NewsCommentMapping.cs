using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsCommentMapping : IEntityTypeConfiguration<NewsComment>
{
#region IEntityTypeConfiguration<NewsComment> Members

    public void Configure(EntityTypeBuilder<NewsComment> builder)
    {
        builder.ToTable("NewsComments");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.NewsContentId).HasMaxLength(40);
        builder.Property(x => x.StaffId).HasMaxLength(40);
    }

#endregion
}