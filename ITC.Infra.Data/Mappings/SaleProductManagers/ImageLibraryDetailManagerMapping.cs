using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class ImageLibraryDetailManagerMapping : IEntityTypeConfiguration<ImageLibraryDetailManager>
{
#region IEntityTypeConfiguration<ImageLibraryDetailManager> Members

    public void Configure(EntityTypeBuilder<ImageLibraryDetailManager> builder)
    {
        builder.ToTable("ImageLibraryDetailManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ImageLibraryManagerId).HasMaxLength(40);
        builder.Property(x => x.Content).HasMaxLength(500);
    }

#endregion
}