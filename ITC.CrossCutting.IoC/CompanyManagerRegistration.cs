using ITC.Application.AppService.CompanyManagers.StaffManagers;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Infra.CrossCutting.Identity.CommandHandlers;
using ITC.Infra.Data.FunctionRepositoryQueries.CompanyManagers.StaffManagers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class CompanyManagerRegistration : Registration
{
#region Constructors

    public CompanyManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

    #region Staff-Manager

        AddScoped<IStaffManagerAppService, StaffManagerAppService>();
        AddScoped<IRequestHandler<AddStaffManagerCommand, bool>, StaffManagerCommandHandler>();
        AddScoped<IRequestHandler<UpdateStaffManagerCommand, bool>, StaffManagerCommandHandler>();
        AddScoped<IRequestHandler<DeleteStaffManagerCommand, bool>, StaffManagerCommandHandler>();
        AddScoped<IRequestHandler<AvatarManagerStaffManagerCommand, bool>, StaffManagerCommandHandler>();
        AddScoped<IStaffManagerRepository, StaffManagerRepository>();
        AddScoped<IStaffManagerQueries>(_ => new StaffManagerQueries(connection));

    #endregion
    }

#endregion
}