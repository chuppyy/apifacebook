#region

using ITC.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace ITC.Infra.Data.Mappings.ChatManager;

public class StoredEventMapping : IEntityTypeConfiguration<StoredEvent>
{
#region IEntityTypeConfiguration<StoredEvent> Members

    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.ToTable("StoredEvent");
        builder.Property(c => c.Timestamp).HasColumnName("CreationDate");
        builder.Property(c => c.MessageType).HasColumnName("Action").HasColumnType("varchar(100)");
        // builder.Property(c => c.Id).Has
    }

#endregion
}