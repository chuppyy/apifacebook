using ITC.Application.AppService.StudyManagers.MinusWord;
using ITC.Domain.CommandHandlers.StudyManagers;
using ITC.Domain.Commands.NewsManagers.MinusWord;
using ITC.Domain.Interfaces.StudyManagers.MinusWord;
using ITC.Infra.Data.FunctionRepositoryQueries.StudyManagers.MinusWord;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class StudyManagerRegistration : Registration
{
#region Constructors

    public StudyManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

    #region MinusWord

        AddScoped<IMinusWordAppService, MinusWordAppService>();
        AddScoped<IRequestHandler<AddMinusWordCommand, bool>, MinusWordCommandHandler>();
        AddScoped<IRequestHandler<UpdateMinusWordCommand, bool>, MinusWordCommandHandler>();
        AddScoped<IRequestHandler<DeleteMinusWordCommand, bool>, MinusWordCommandHandler>();
        AddScoped<IMinusWordRepository, MinusWordRepository>();
        AddScoped<IMinusWordQueries>(_ => new MinusWordQueries(connection));

    #endregion
    }

#endregion
}