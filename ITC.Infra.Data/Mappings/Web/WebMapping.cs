using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.Web
{
    public class WebMapping : IEntityTypeConfiguration<Domain.Models.Web.Web>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Web.Web> builder)
        {
            builder.ToTable("Webs");
            builder.HasKey(x => x.Id);
        }
    }
}
