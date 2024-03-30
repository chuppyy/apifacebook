using ITC.Domain.Models.MenuManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.NewsManagers;

public class MinusWordMapping : IEntityTypeConfiguration<MinusWord>
{
#region IEntityTypeConfiguration<MinusWord> Members

    public void Configure(EntityTypeBuilder<MinusWord> builder)
    {
        builder.ToTable("MinusWords");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Root).HasMaxLength(500);
        builder.Property(x => x.Replace).HasMaxLength(500);
        builder.Property(x => x.CreatedBy).HasMaxLength(40);
        builder.Property(x => x.ModifiedBy).HasMaxLength(40);
    }

#endregion
}