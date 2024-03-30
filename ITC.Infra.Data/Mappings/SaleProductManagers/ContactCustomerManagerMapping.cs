using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class ContactCustomerManagerMapping : IEntityTypeConfiguration<ContactCustomerManager>
{
#region IEntityTypeConfiguration<ContactCustomerManager> Members

    public void Configure(EntityTypeBuilder<ContactCustomerManager> builder)
    {
        builder.ToTable("ContactCustomerManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Phone).HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(250);
        builder.Property(x => x.HandlerUser).HasMaxLength(40);
    }

#endregion
}