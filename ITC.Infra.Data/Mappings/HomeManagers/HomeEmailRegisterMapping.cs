using ITC.Domain.Models.HomeManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.HomeManagers;

public class HomeEmailRegisterMapping : IEntityTypeConfiguration<HomeEmailRegister>
{
#region IEntityTypeConfiguration<HomeEmailRegister> Members

    public void Configure(EntityTypeBuilder<HomeEmailRegister> builder)
    {
        builder.ToTable("HomeEmailRegisters");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Email).HasMaxLength(250);
    }

#endregion
}