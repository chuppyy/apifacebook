using ITC.Domain.Models.NewsManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class NewsDomainMapping : IEntityTypeConfiguration<NewsDomain>
{
#region IEntityTypeConfiguration<NewsDomain> Members

    public void Configure(EntityTypeBuilder<NewsDomain> builder)
    {
        builder.ToTable("NewsDomains");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.DomainNew).HasMaxLength(100);
        builder.Property(x => x.CreatedBy).HasMaxLength(40);
        builder.Property(x => x.ModifiedBy).HasMaxLength(40);
    }

#endregion
}