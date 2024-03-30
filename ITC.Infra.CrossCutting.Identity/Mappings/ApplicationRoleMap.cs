#region

using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Mappings;

public class ApplicationRoleMap : IEntityTypeConfiguration<ApplicationRole>
{
#region IEntityTypeConfiguration<ApplicationRole> Members

    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.Property(c => c.Identity)
               .HasColumnName("Identity")
               .IsRequired();
    }

#endregion
}