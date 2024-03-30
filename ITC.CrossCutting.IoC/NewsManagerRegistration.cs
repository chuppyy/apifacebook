using ITC.Application.AppService.NewsManagers.NewsAttackManagers;
using ITC.Application.AppService.NewsManagers.NewsContentManagers;
using ITC.Application.AppService.NewsManagers.NewsDomainManagers;
using ITC.Application.AppService.NewsManagers.NewsGithubManagers;
using ITC.Application.AppService.NewsManagers.NewsGroupManagers;
using ITC.Application.AppService.NewsManagers.NewsGroupTypeManagers;
using ITC.Application.AppService.NewsManagers.NewsRecruitmentManagers;
using ITC.Application.AppService.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Application.AppService.NewsManagers.NewsViaManagers;
using ITC.Application.Schedule;
using ITC.Application.Services.Vercel;
using ITC.Domain.CommandHandlers.NewsManagers;
using ITC.Domain.Commands.NewsManagers.NewsContentManagers;
using ITC.Domain.Commands.NewsManagers.NewsDomainManagers;
using ITC.Domain.Commands.NewsManagers.NewsGithubManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Commands.NewsManagers.NewsVia;
using ITC.Domain.Interfaces.NewsManagers.NewsAttackManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsConfigManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGithubManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsVercelManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsViaManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsAttackManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsConfigManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsContentManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsDomainManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGithubManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGroupManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsGroupTypeManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsRecruitmentManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsVercelManagers;
using ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsViaManagers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class NewsManagerRegistration : Registration
{
    #region Constructors

    public NewsManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

        #region News-Group

        AddScoped<INewsGroupAppService, NewsGroupAppService>();
        AddScoped<IRequestHandler<AddNewsGroupCommand, bool>, NewsGroupCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsGroupCommand, bool>, NewsGroupCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsGroupCommand, bool>, NewsGroupCommandHandler>();
        AddScoped<INewsGroupRepository, NewsGroupRepository>();
        AddScoped<INewsGroupQueries>(_ => new NewsGroupQueries(connection));

        #endregion

        #region News-Group-Type

        AddScoped<INewsGroupTypeAppService, NewsGroupTypeAppService>();
        AddScoped<IRequestHandler<AddNewsGroupTypeCommand, bool>, NewsGroupTypeCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsGroupTypeCommand, bool>, NewsGroupTypeCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsGroupTypeCommand, bool>, NewsGroupTypeCommandHandler>();
        AddScoped<INewsGroupTypeRepository, NewsGroupTypeRepository>();
        AddScoped<INewsGroupTypeQueries>(_ => new NewsGroupTypeQueries(connection));

        #endregion

        #region News-Content

        AddScoped<INewsContentAppService, NewsContentAppService>();
        AddScoped<IPostFaceService, PostFaceService>();
        AddScoped<IVercelService, VercelService>();
        AddScoped<IRequestHandler<AddNewsContentCommand, bool>, NewsContentCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsContentCommand, bool>, NewsContentCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsContentCommand, bool>, NewsContentCommandHandler>();
        AddScoped<IRequestHandler<UpdateTimeAutoPostNewsContentCommand, bool>, NewsContentCommandHandler>();
        AddScoped<INewsContentRepository, NewsContentRepository>();
        AddScoped<INewsContentQueries>(_ => new NewsContentQueries(connection));


        AddScoped<INewsConfigRepository, NewsConfigRepository>();
        AddScoped<INewsVercelQueries>(_ => new NewsVercelQueries(connection));

        #endregion

        #region News-Attack

        AddScoped<INewsAttackAppService, NewsAttackAppService>();
        AddScoped<INewsAttackRepository, NewsAttackRepository>();
        AddScoped<INewsAttackQueries>(_ => new NewsAttackQueries(connection));

        #endregion

        #region News-Seo-KeyWord

        AddScoped<INewsSeoKeyWordAppService, NewsSeoKeyWordAppService>();
        AddScoped<IRequestHandler<AddNewsSeoKeyWordCommand, bool>, NewsSeoKeyWordCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsSeoKeyWordCommand, bool>, NewsSeoKeyWordCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsSeoKeyWordCommand, bool>, NewsSeoKeyWordCommandHandler>();
        AddScoped<INewsSeoKeyWordRepository, NewsSeoKeyWordRepository>();
        AddScoped<INewsSeoKeyWordQueries>(_ => new NewsSeoKeyWordQueries(connection));

        #endregion

        #region NewsRecruitment

        AddScoped<INewsRecruitmentAppService, NewsRecruitmentAppService>();
        AddScoped<IRequestHandler<AddNewsRecruitmentCommand, bool>, NewsRecruitmentCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsRecruitmentCommand, bool>, NewsRecruitmentCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsRecruitmentCommand, bool>, NewsRecruitmentCommandHandler>();
        AddScoped<INewsRecruitmentRepository, NewsRecruitmentRepository>();
        AddScoped<INewsRecruitmentQueries>(_ => new NewsRecruitmentQueries(connection));

        #endregion

        #region News-Via

        AddScoped<INewsViaAppService, NewsViaAppService>();
        AddScoped<IRequestHandler<AddNewsViaCommand, bool>, NewsViaCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsViaCommand, bool>, NewsViaCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsViaCommand, bool>, NewsViaCommandHandler>();
        AddScoped<INewsViaRepository, NewsViaRepository>();
        AddScoped<INewsViaQueries>(_ => new NewsViaQueries(connection));

        #endregion

        #region NewsGithub

        AddScoped<INewsGithubAppService, NewsGithubAppService>();
        AddScoped<IRequestHandler<AddNewsGithubCommand, bool>, NewsGithubCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsGithubCommand, bool>, NewsGithubCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsGithubCommand, bool>, NewsGithubCommandHandler>();
        AddScoped<INewsGithubRepository, NewsGithubRepository>();
        AddScoped<INewsGithubQueries>(_ => new NewsGithubQueries(connection));

        #endregion

        #region NewsDomain

        AddScoped<INewsDomainAppService, NewsDomainAppService>();
        AddScoped<IRequestHandler<AddNewsDomainCommand, bool>, NewsDomainCommandHandler>();
        AddScoped<IRequestHandler<UpdateNewsDomainCommand, bool>, NewsDomainCommandHandler>();
        AddScoped<IRequestHandler<DeleteNewsDomainCommand, bool>, NewsDomainCommandHandler>();
        AddScoped<IRequestHandler<SchedulerNewsDomainCommand, bool>, NewsDomainCommandHandler>();
        AddScoped<INewsDomainRepository, NewsDomainRepository>();
        AddScoped<INewsDomainQueries>(_ => new NewsDomainQueries(connection));

        #endregion

        AddScoped<ISchedulerManager, SchedulerManager>();
    }

    #endregion
}