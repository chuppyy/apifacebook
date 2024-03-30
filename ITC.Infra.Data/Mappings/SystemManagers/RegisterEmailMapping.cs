using ITC.Domain.Models.SystemManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SystemManagers;

public class RegisterEmailMapping : IEntityTypeConfiguration<RegisterEmail>
{
    public void Configure(EntityTypeBuilder<RegisterEmail> builder)
    {
        builder.ToTable("RegisterEmails");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Email).HasMaxLength(250);
    }
}