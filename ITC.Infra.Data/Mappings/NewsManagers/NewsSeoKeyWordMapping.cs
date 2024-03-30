using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsSeoKeyWordMapping : IEntityTypeConfiguration<NewsSeoKeyWord>
{
#region IEntityTypeConfiguration<NewsSeoKeyWord> Members

    public void Configure(EntityTypeBuilder<NewsSeoKeyWord> builder)
    {
        builder.ToTable("NewsSeoKeyWords");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
    }

#endregion
}