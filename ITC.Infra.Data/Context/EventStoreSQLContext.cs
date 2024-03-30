#region

using ITC.Domain.Core.Events;
using ITC.Infra.Data.Mappings.ChatManager;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.Data.Context;

public class EventStoreSqlContext : DbContext
{
#region Constructors

    public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options)
    {
    }

#endregion

#region Properties

    public DbSet<StoredEvent> StoredEvents { get; set; }

#endregion

#region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoredEventMapping());
        base.OnModelCreating(modelBuilder);
    }

#endregion
}