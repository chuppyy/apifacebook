using ITC.Domain.Models.SaleProductManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.SaleProductManagers;

public class CommentManagerMapping : IEntityTypeConfiguration<CommentManager>
{
#region IEntityTypeConfiguration<CommentManager> Members

    public void Configure(EntityTypeBuilder<CommentManager> builder)
    {
        builder.ToTable("CommentManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.ProductId).HasMaxLength(40);
        builder.Property(x => x.ParentId).HasMaxLength(40);
        builder.Property(x => x.UserAgree).HasMaxLength(40);
        builder.Property(x => x.SecrectKey).HasMaxLength(40);
        builder.Property(x => x.Email).HasMaxLength(250);
        builder.Property(x => x.Phone).HasMaxLength(20);
    }

#endregion
}