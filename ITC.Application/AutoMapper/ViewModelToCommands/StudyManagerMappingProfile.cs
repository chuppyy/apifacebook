using AutoMapper;
using ITC.Domain.Commands.NewsManagers.MinusWord;
using ITC.Domain.Commands.SystemManagers.NotificationManagers;
using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;
using ITC.Domain.Core.ModelShare.SystemManagers.NotificationManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     StudyManagerMappingProfile
/// </summary>
public class StudyManagerMappingProfile : Profile
{
    public StudyManagerMappingProfile()
    {
        //====== NotificationManagerManager ===================================
        CreateMap<NotificationManagerEventModel, AddNotificationManagerCommand>()
            .ConstructUsing(c => new AddNotificationManagerCommand(c));
        CreateMap<NotificationManagerEventModel, UpdateNotificationManagerCommand>()
            .ConstructUsing(c => new UpdateNotificationManagerCommand(c));
        CreateMap<NotificationManagerEventModel, UpdateReadNotificationManagerCommand>()
            .ConstructUsing(c => new UpdateReadNotificationManagerCommand(c));

        //====== MinusWord ===================================
        CreateMap<MinusWordEventModel, AddMinusWordCommand>().ConstructUsing(c => new AddMinusWordCommand(c));
        CreateMap<MinusWordEventModel, UpdateMinusWordCommand>().ConstructUsing(c => new UpdateMinusWordCommand(c));
    }
}