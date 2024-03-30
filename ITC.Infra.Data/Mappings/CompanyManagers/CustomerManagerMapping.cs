using ITC.Domain.Models.CompanyManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.CompanyManagers;

public class CustomerManagerMapping : IEntityTypeConfiguration<CustomerManager>
{
#region IEntityTypeConfiguration<CustomerManager> Members

    public void Configure(EntityTypeBuilder<CustomerManager> builder)
    {
        builder.ToTable("CustomerManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.CompanyId).HasMaxLength(40);
    }

#endregion
}