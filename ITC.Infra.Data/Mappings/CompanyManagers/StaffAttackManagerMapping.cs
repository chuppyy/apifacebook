using ITC.Domain.Models.CompanyManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.CompanyManagers;

public class StaffAttackManagerMapping : IEntityTypeConfiguration<StaffAttackManager>
{
    public void Configure(EntityTypeBuilder<StaffAttackManager> builder)
    {
        builder.ToTable("StaffAttackManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.StaffManagerId).HasMaxLength(40);
        builder.Property(x => x.FileId).HasMaxLength(40);
    }
}