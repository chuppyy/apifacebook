using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class ContactManagerMapping : IEntityTypeConfiguration<ContactManager>
{
#region IEntityTypeConfiguration<ContactManager> Members

    public void Configure(EntityTypeBuilder<ContactManager> builder)
    {
        builder.ToTable("ContactManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.Phone).HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(250);
        builder.Property(x => x.Skype).HasMaxLength(100);
        builder.Property(x => x.Zalo).HasMaxLength(100);
        builder.Property(x => x.GoogleMap).HasMaxLength(1000);
        builder.Property(x => x.GoogleMapLink).HasMaxLength(500);
        builder.Property(x => x.TimeWork).HasMaxLength(300);
        builder.Property(x => x.Hotline).HasMaxLength(50);
        builder.Property(x => x.Facebook).HasMaxLength(300);
        builder.Property(x => x.Youtube).HasMaxLength(300);
        builder.Property(x => x.Twitter).HasMaxLength(300);
        builder.Property(x => x.Linkedin).HasMaxLength(300);
    }

#endregion
}