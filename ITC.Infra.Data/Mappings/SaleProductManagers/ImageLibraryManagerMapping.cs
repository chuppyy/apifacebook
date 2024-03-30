using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class ImageLibraryManagerMapping : IEntityTypeConfiguration<ImageLibraryManager>
{
#region IEntityTypeConfiguration<ImageLibraryManager> Members

    public void Configure(EntityTypeBuilder<ImageLibraryManager> builder)
    {
        builder.ToTable("ImageLibraryManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.AvatarId).HasMaxLength(40);
        builder.Property(x => x.Content).HasMaxLength(500);
        builder.Property(x => x.SecretKey).HasMaxLength(20);
    }

#endregion
}