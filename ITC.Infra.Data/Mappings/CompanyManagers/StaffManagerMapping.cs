using ITC.Domain.Models.CompanyManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.CompanyManagers;

public class StaffManagerMapping : IEntityTypeConfiguration<StaffManager>
{
#region IEntityTypeConfiguration<StaffManager> Members

    public void Configure(EntityTypeBuilder<StaffManager> builder)
    {
        builder.ToTable("StaffManagers");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.ManagementId).HasMaxLength(40);
        builder.Property(x => x.RoomManagerId).HasMaxLength(40);
        builder.Property(x => x.UserTypeManagerId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.MissionId).HasMaxLength(40);
        builder.Property(x => x.PositionId).HasMaxLength(40);
        builder.Property(x => x.SchoolClass).HasMaxLength(10);
        builder.Property(x => x.SchoolName).HasMaxLength(150);
        builder.Property(x => x.CitizenIdentification).HasMaxLength(12);
        builder.Property(x => x.IdentityNumberStudy).HasMaxLength(40);
        builder.Property(x => x.NationPeople).HasMaxLength(20);
        builder.Property(x => x.UserCode).HasMaxLength(50);
        builder.Property(x => x.BirthDay).IsRequired(false);
    }

#endregion
}