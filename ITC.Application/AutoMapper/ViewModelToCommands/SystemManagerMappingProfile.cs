using AutoMapper;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Commands.SystemManagers.RegisterEmailManagers;
using ITC.Domain.Commands.SystemManagers.ServerFileManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.RegisterEmailManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using NCore.Modals;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

/// <summary>
///     MenuManagerMappingProfile
/// </summary>
public class SystemManagerMappingProfile : Profile
{
    public SystemManagerMappingProfile()
    {
        //====== ServerFile ===================================
        CreateMap<UploadFileEventModel, UploadServerFileCommand>().ConstructUsing(c => new UploadServerFileCommand(c));
        CreateMap<UploadFileEventModel, UploadServerFile2023Command>().ConstructUsing(c => new UploadServerFile2023Command(c));
        CreateMap<UploadFileEventModel, UploadServerFileAttackCommand>()
            .ConstructUsing(c => new UploadServerFileAttackCommand(c));
        CreateMap<UpdateFileNameModal, UpdateFileNameServerFileCommand>()
            .ConstructUsing(c => new UpdateFileNameServerFileCommand(c));
        CreateMap<FolderServerFileEvent, AddFolderServerFileCommand>()
            .ConstructUsing(c => new AddFolderServerFileCommand(c));
        CreateMap<FolderServerFileEvent, UpdateFolderServerFileCommand>()
            .ConstructUsing(c => new UpdateFolderServerFileCommand(c));
        CreateMap<FolderServerFileEvent, UpdateParentServerFileCommand>()
            .ConstructUsing(c => new UpdateParentServerFileCommand(c));
        CreateMap<UploadDifferenceEventModal, UploadDifferenceServerFileCommand>()
            .ConstructUsing(c => new UploadDifferenceServerFileCommand(c));
        CreateMap<UpdateFileNameModal, UpdateFileNameServerFileCommand>()
            .ConstructUsing(c => new UpdateFileNameServerFileCommand(c));

        //====== Helper ===================================
        CreateMap<UpdateStatusHelperModal, UpdateStatusHelperCommand>()
            .ConstructUsing(c => new UpdateStatusHelperCommand(c));

        //====== RegisterEmail ===================================
        CreateMap<RegisterEmailEventModel, RegRegisterEmailCommand>()
            .ConstructUsing(c => new RegRegisterEmailCommand(c));
    }
}