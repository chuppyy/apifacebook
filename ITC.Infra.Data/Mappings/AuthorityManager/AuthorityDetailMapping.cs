using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class AuthorityDetailMapping : IEntityTypeConfiguration<AuthorityDetail>
{
#region IEntityTypeConfiguration<AuthorityDetail> Members

    public void Configure(EntityTypeBuilder<AuthorityDetail> builder)
    {
        builder.ToTable("AuthorityDetail");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.MenuManagerId).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.AuthorityId).HasMaxLength(40).ValueGeneratedNever();
    }

#endregion
}