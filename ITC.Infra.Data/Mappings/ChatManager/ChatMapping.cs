using ITC.Domain.Models.ChatManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITC.Infra.Data.Mappings.ChatManager;

public class ChatMapping : IEntityTypeConfiguration<Chat>
{
#region IEntityTypeConfiguration<Position> Members

    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
        builder.Property(x => x.Id).HasMaxLength(40);
        builder.Property(x => x.Sender).HasMaxLength(40);
        builder.Property(x => x.Receiveder).HasMaxLength(40);
    }

#endregion
}