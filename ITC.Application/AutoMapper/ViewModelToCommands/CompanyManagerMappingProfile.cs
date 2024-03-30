using AutoMapper;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Commands.Itphonui.ProjectAccountManagers;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     CompanyManagerMappingProfile
/// </summary>
public class CompanyManagerMappingProfile : Profile
{
    public CompanyManagerMappingProfile()
    {
        //====== StaffManager ===================================
        CreateMap<StaffManagerEventModel, AddStaffManagerCommand>()
            .ConstructUsing(c => new AddStaffManagerCommand(c));
        CreateMap<StaffManagerEventModel, UpdateStaffManagerCommand>()
            .ConstructUsing(c => new UpdateStaffManagerCommand(c));
        CreateMap<UploadImageStaffEventModel, AvatarManagerStaffManagerCommand>()
            .ConstructUsing(c => new AvatarManagerStaffManagerCommand(c));

        CreateMap<ProjectAccountManagerEventModel, AddProjectAccountManagerCommand>()
            .ConstructUsing(c => new AddProjectAccountManagerCommand(c));
        CreateMap<ProjectAccountManagerEventModel, UpdateProjectAccountManagerCommand>()
            .ConstructUsing(c => new UpdateProjectAccountManagerCommand(c));
    }
}