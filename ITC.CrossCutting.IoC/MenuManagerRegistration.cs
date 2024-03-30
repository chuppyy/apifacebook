using ITC.Application.AppService.SystemManagers.ServerFileManagers;
using ITC.Domain.CommandHandlers.SystemManagers;
using ITC.Domain.Commands.SystemManagers.ServerFileManagers;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.ServerFileManagers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class MenuManagerRegistration : Registration
{
#region Constructors

    public MenuManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

    #region ServerFile-Manager

        AddScoped<IServerFileAppService, ServerFileAppService>();
        AddScoped<IRequestHandler<UploadServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UploadServerFile2023Command, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UploadServerFileAttackCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<DeleteServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UpdateFileNameServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UpdateParentServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<AddFolderServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UpdateFolderServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<UploadDifferenceServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IRequestHandler<CallDeQuyServerFileCommand, bool>, ServerFileCommandHandler>();
        AddScoped<IServerFileRepository, ServerFileRepository>();
        AddScoped<IServerFileQueries>(_ => new ServerFileQueries(connection));

    #endregion
    }

#endregion
}