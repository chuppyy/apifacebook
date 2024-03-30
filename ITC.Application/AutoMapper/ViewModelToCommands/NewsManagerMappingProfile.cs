using AutoMapper;
using ITC.Domain.Commands.NewsManagers.NewsContentManagers;
using ITC.Domain.Commands.NewsManagers.NewsDomainManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupManagers;
using ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     NewsManagerMappingProfile
/// </summary>
public class NewsManagerMappingProfile : Profile
{
    public NewsManagerMappingProfile()
    {
        //====== NewsGroup ===================================
        CreateMap<NewsGroupEventModel, AddNewsGroupCommand>().ConstructUsing(c => new AddNewsGroupCommand(c));
        CreateMap<NewsGroupEventModel, UpdateNewsGroupCommand>().ConstructUsing(c => new UpdateNewsGroupCommand(c));

        //====== NewsGroupType ===================================
        CreateMap<NewsGroupTypeEventModel, AddNewsGroupTypeCommand>()
            .ConstructUsing(c => new AddNewsGroupTypeCommand(c));
        CreateMap<NewsGroupTypeEventModel, UpdateNewsGroupTypeCommand>()
            .ConstructUsing(c => new UpdateNewsGroupTypeCommand(c));

        //====== NewsContent ===================================
        CreateMap<NewsContentEventModel, AddNewsContentCommand>().ConstructUsing(c => new AddNewsContentCommand(c));
        CreateMap<NewsContentEventModel, UpdateNewsContentCommand>().ConstructUsing(c => new UpdateNewsContentCommand(c));
        CreateMap<NewsContentUpdateTimeAutoPostModel, UpdateTimeAutoPostNewsContentCommand>().ConstructUsing(c => new UpdateTimeAutoPostNewsContentCommand(c));

        //====== NewsSeoKeyWord ===================================
        CreateMap<NewsSeoKeyWordEventModel, AddNewsSeoKeyWordCommand>()
            .ConstructUsing(c => new AddNewsSeoKeyWordCommand(c));
        CreateMap<NewsSeoKeyWordEventModel, UpdateNewsSeoKeyWordCommand>()
            .ConstructUsing(c => new UpdateNewsSeoKeyWordCommand(c));

        //====== NewsRecruitment ===================================
        CreateMap<NewsRecruitmentEventModel, AddNewsRecruitmentCommand>()
            .ConstructUsing(c => new AddNewsRecruitmentCommand(c));
        CreateMap<NewsRecruitmentEventModel, UpdateNewsRecruitmentCommand>()
            .ConstructUsing(c => new UpdateNewsRecruitmentCommand(c));
        
        //====== NewsDomain ===================================
        CreateMap<NewsDomainEventModel, AddNewsDomainCommand>().ConstructUsing(c => new AddNewsDomainCommand(c));
        CreateMap<NewsDomainEventModel, UpdateNewsDomainCommand>().ConstructUsing(c => new UpdateNewsDomainCommand(c));
        CreateMap<NewsDomainSchedulerEvent, SchedulerNewsDomainCommand>().ConstructUsing(c => new SchedulerNewsDomainCommand(c));
    }
}