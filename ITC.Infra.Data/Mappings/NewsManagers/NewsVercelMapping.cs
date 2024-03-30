using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsVercelMapping : IEntityTypeConfiguration<NewsVercel>
{
#region IEntityTypeConfiguration<NewsVercel> Members

    public void Configure(EntityTypeBuilder<NewsVercel> builder)
    {
        builder.ToTable("NewsVercels");
        builder.Property(x => x.Id).HasColumnName("Id").HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
    }
#endregion
}