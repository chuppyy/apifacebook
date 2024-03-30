using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsViaMapping : IEntityTypeConfiguration<NewsVia>
{
#region IEntityTypeConfiguration<NewsVia> Members

    public void Configure(EntityTypeBuilder<NewsVia> builder)
    {
        builder.ToTable("NewsVias");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Code).HasMaxLength(50);
        builder.Property(x => x.Token).HasMaxLength(500);
        builder.Property(x => x.IdTkQc).HasMaxLength(40);
        builder.Property(x => x.StaffId).HasMaxLength(40);
        builder.Property(x => x.CreatedBy).HasMaxLength(40);
        builder.Property(x => x.ModifiedBy).HasMaxLength(40);
    }

#endregion
}