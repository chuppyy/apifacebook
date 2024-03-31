using ITC.Domain.Models.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Web
{
    public class ConfigAnalyticMapping : IEntityTypeConfiguration<ConfigAnalytic>
    {
        public void Configure(EntityTypeBuilder<ConfigAnalytic> builder)
        {
            builder.ToTable("ConfigAnalytics");
            builder.HasKey(x => x.Id);
        }
    }
}
