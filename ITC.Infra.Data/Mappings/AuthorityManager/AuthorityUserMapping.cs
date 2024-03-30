using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class AuthorityUserMapping : IEntityTypeConfiguration<AuthorityUser>
{
#region IEntityTypeConfiguration<AuthorityUser> Members

    public void Configure(EntityTypeBuilder<AuthorityUser> builder)
    {
        builder.ToTable("AuthorityUsers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.CompanyId).HasMaxLength(40);
        builder.Property(x => x.AuthorityId).HasMaxLength(40);
        builder.Property(x => x.UserId).HasMaxLength(40);
    }

#endregion
}