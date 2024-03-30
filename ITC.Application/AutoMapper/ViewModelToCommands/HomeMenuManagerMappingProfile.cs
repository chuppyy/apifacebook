using AutoMapper;
using ITC.Domain.Commands.HomeManagers.HomeMenuManagers;
using ITC.Domain.Commands.HomeManagers.HomeNewsGroupViewManagers;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeMenuManagers;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     HomeManagerMappingProfile
/// </summary>
public class HomeManagerMappingProfile : Profile
{
    public HomeManagerMappingProfile()
    {
        //====== Home-Menu ===================================
        CreateMap<HomeMenuEventModel, AddHomeMenuCommand>().ConstructUsing(c => new AddHomeMenuCommand(c));
        CreateMap<HomeMenuEventModel, UpdateHomeMenuCommand>().ConstructUsing(c => new UpdateHomeMenuCommand(c));

        //====== Home-News-Group-View ===================================
        CreateMap<HomeNewsGroupViewEventModel, AddHomeNewsGroupViewCommand>()
            .ConstructUsing(c => new AddHomeNewsGroupViewCommand(c));
        CreateMap<HomeNewsGroupViewEventModel, UpdateHomeNewsGroupViewCommand>()
            .ConstructUsing(c => new UpdateHomeNewsGroupViewCommand(c));
    }
}