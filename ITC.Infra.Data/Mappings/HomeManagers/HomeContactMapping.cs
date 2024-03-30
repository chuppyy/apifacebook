using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeContactMapping : IEntityTypeConfiguration<HomeContact>
{
#region IEntityTypeConfiguration<HomeContact> Members

    public void Configure(EntityTypeBuilder<HomeContact> builder)
    {
        builder.ToTable("HomeContacts");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.PhoneNumberCall).HasMaxLength(50);
        builder.Property(x => x.PhoneNumberView).HasMaxLength(50);
        builder.Property(x => x.Zalo).HasMaxLength(100);
        builder.Property(x => x.Facebook).HasMaxLength(250);
        builder.Property(x => x.Email).HasMaxLength(250);
    }

#endregion
}