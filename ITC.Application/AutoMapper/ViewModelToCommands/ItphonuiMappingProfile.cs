using AutoMapper;
using ITC.Domain.Commands.Itphonui.ManagementManagers;
using ITC.Domain.Commands.Itphonui.ProjectManager;
using ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;
using ITC.Domain.Core.ModelShare.Itphonui.ProjectManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     ItphonuiMappingProfile
/// </summary>
public class ItphonuiMappingProfile : Profile
{
    public ItphonuiMappingProfile()
    {
        //====== ProjectManager ===================================
        CreateMap<ProjectManagerEventModel, AddProjectManagerCommand>()
            .ConstructUsing(c => new AddProjectManagerCommand(c));
        CreateMap<ProjectManagerEventModel, UpdateProjectManagerCommand>()
            .ConstructUsing(c => new UpdateProjectManagerCommand(c));

        //====== ManagementManager ===================================
        CreateMap<ManagementManagerEventModel, AddManagementManagerCommand>()
            .ConstructUsing(c => new AddManagementManagerCommand(c));
        CreateMap<ManagementManagerEventModel, UpdateManagementManagerCommand>()
            .ConstructUsing(c => new UpdateManagementManagerCommand(c));
        CreateMap<ManagementDetailManagerEventModel, AddManagementDetailManagerCommand>()
            .ConstructUsing(c => new AddManagementDetailManagerCommand(c));
    }
}