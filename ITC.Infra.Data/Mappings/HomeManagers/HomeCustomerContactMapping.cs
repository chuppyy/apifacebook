using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeCustomerContactMapping : IEntityTypeConfiguration<HomeCustomerContact>
{
#region IEntityTypeConfiguration<HomeCustomerContact> Members

    public void Configure(EntityTypeBuilder<HomeCustomerContact> builder)
    {
        builder.ToTable("HomeCustomerContacts");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.PhoneNumber).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(250);
    }

#endregion
}