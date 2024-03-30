using AutoMapper;
using ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Commands.AuthorityManager.IconManagerSystem;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;

namespace ITC.Application.AutoMapper.ViewModelToCommands;

public class AuthorityManagerMappingProfile : Profile
{
    public AuthorityManagerMappingProfile()
    {
        //=====ManagerIcon==============
        CreateMap<ManagerIconEventModel, AddManagerIconCommand>()
            .ConstructUsing(c => new AddManagerIconCommand(c.Name));
        CreateMap<ManagerIconEventModel, UpdateManagerIconCommand>()
            .ConstructUsing(c => new UpdateManagerIconCommand(c.Id, c.Name));

        //=====MenuManager==============
        CreateMap<MenuManagerEventModel, AddMenuManagerCommand>()
            .ConstructUsing(c => new AddMenuManagerCommand(c.ManagerICon, c.Name, c.MenuGroupId, c.Position,
                                                           c.Router, c.ParentId,
                                                           c.PermissionValue, c.Code, ""));
        CreateMap<MenuManagerEventModel, UpdateMenuManagerCommand>()
            .ConstructUsing(c => new UpdateMenuManagerCommand(c.Id, c.ProjectId, c.ManagerICon, c.Name,
                                                              c.MenuGroupId, c.Position, c.Router,
                                                              c.ParentId, c.PermissionValue, c.Code, ""));

        CreateMap<AuthoritiesMenuManagerEventModel, AddAuthoritiesMenuManagerCommand>()
            .ConstructUsing(c => new AddAuthoritiesMenuManagerCommand(c));
        CreateMap<AuthoritiesMenuManagerEventModel, UpdateAuthoritiesMenuManagerCommand>()
            .ConstructUsing(c => new UpdateAuthoritiesMenuManagerCommand(c));

        CreateMap<SortMenuManagerEventModel, SortMenuManagerCommand>()
            .ConstructUsing(c => new SortMenuManagerCommand(c));

        //=====AuthorityManagerSystem==============
        CreateMap<AuthorityManagerSystemEventModel, AddAuthorityManagerSystemCommand>()
            .ConstructUsing(c => new AddAuthorityManagerSystemCommand(c));
        CreateMap<AuthorityManagerSystemEventModel, UpdateAuthorityManagerSystemCommand>()
            .ConstructUsing(c => new UpdateAuthorityManagerSystemCommand(c));
        CreateMap<AuthorityManagerSystemUpdatePermissionEventModel, UpdateAuthorityManagerSystemPermissionMenuCommand>()
            .ConstructUsing(c => new UpdateAuthorityManagerSystemPermissionMenuCommand(c));

        //=====UpdatePermissionByAuthoritiesCommand==============
        CreateMap<SortMenuPermissionByAuthoritiesModel, UpdatePermissionByAuthoritiesCommand>()
            .ConstructUsing(c => new UpdatePermissionByAuthoritiesCommand(c));
    }
}