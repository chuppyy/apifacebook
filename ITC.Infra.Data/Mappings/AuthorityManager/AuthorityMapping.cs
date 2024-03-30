﻿using ITC.Domain.Models.AuthorityManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.AuthorityManager;

public class AuthorityMapping : IEntityTypeConfiguration<Authority>
{
#region IEntityTypeConfiguration<Authority> Members

    public void Configure(EntityTypeBuilder<Authority> builder)
    {
        builder.ToTable("Authorities");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.CompanyId).HasMaxLength(40);
        builder.Property(x => x.ProjectId).HasMaxLength(40);
        builder.Property(x => x.UrlHomePage).HasMaxLength(500);
    }

#endregion
}