#region

using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Infra.CrossCutting.Bus;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.EventSourcing;
using ITC.Infra.Data.Repositories;
using ITC.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ITC.Infra.CrossCutting.IoC;

public class GeneralRegistration : Registration
{
#region Constructors

    public GeneralRegistration(IServiceCollection services) : base(services)
    {
    }

#endregion

#region Methods

    public override void Register(IConfiguration configuration)
    {
        // Domain Bus (Mediator)
        AddScoped<EQMContext>();
        AddScoped<IMediatorHandler, InMemoryBus>();
        AddScoped<IUnitOfWork, UnitOfWork>();

        // Domain - Events
        AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();


        // Infra - Data EventSourcing
        AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
        AddScoped<IEventStore, SqlEventStore>();
        AddScoped<EventStoreSqlContext>();
    }

#endregion
}