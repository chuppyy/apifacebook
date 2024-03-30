using Microsoft.EntityFrameworkCore;
using ITC.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.TokenTwitterManagers
{
    public class TokenTwitterMapping : IEntityTypeConfiguration<TokenTwitter>
    {
        public void Configure(EntityTypeBuilder<TokenTwitter> builder)
        {
            builder.ToTable("TokenTwitters");
            builder.Property(x => x.Id).HasMaxLength(40);
            builder.Property(x => x.ApiKey).HasMaxLength(100);
            builder.Property(x => x.ApiSecret).HasMaxLength(100);
            builder.Property(x => x.Token).HasMaxLength(100);
            builder.Property(x => x.TokenSecret).HasMaxLength(100);
            builder.Property(x => x.AmountPosted).HasDefaultValue(0);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.ModifiedDate);
        }
    }
}
