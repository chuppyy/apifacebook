using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsGroupMapping : IEntityTypeConfiguration<NewsGroup>
{
#region IEntityTypeConfiguration<NewsGroup> Members

    public void Configure(EntityTypeBuilder<NewsGroup> builder)
    {
        builder.ToTable("NewsGroups");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
        builder.Property(x => x.NewsGroupTypeId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Domain).HasMaxLength(500);
        builder.Property(x => x.MetaTitle).HasMaxLength(500);
        builder.Property(x => x.IdViaQc).HasMaxLength(40);
        builder.Property(x => x.StaffId).HasMaxLength(40);
        builder.Property(x => x.LinkTree).HasMaxLength(500);
        builder.Property(x => x.DomainVercel).HasMaxLength(500);
    }

#endregion
}