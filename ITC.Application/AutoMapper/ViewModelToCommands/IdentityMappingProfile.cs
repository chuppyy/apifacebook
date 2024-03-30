#region

using AutoMapper;
using ITC.Application.ViewModels.Account;
using ITC.Application.ViewModels.ManageRole;
using ITC.Domain.Commands.ManageRole;
using ITC.Domain.Commands.ManageRole.Account;
using ITC.Domain.Commands.ManageRole.OfficalAccount;
using ITC.Infra.CrossCutting.Identity.Commands.Module;
using ITC.Infra.CrossCutting.Identity.Commands.Portal;

#endregion

namespace ITC.Application.AutoMapper.ViewModelToCommands;

public class IdentityMappingProfile : Profile
{
#region Constructors

    public IdentityMappingProfile()
    {
    #region Manage Role Identity

        CreateMap<ModuleViewModel, AddModuleCommand>()
            .ConstructUsing(c => new AddModuleCommand(c.Name, c.Identity, c.Description, c.RoleIds, c.Weights));
        CreateMap<ModuleViewModel, UpdateModuleCommand>()
            .ConstructUsing(c => new UpdateModuleCommand(c.Name, c.Identity, c.Description, c.RoleIds, c.GroupId,
                                                         c.Weights));

        CreateMap<FunctionViewModel, AddFunctionCommand>()
            .ConstructUsing(c => new AddFunctionCommand(c.Name, c.Weight, c.Description, c.ModuleId));
        CreateMap<FunctionViewModel, UpdateFunctionCommand>()
            .ConstructUsing(c => new UpdateFunctionCommand(c.Name, c.Weight, c.Description, c.ModuleId));

        CreateMap<AccountViewModel, AddAccountCommand>()
            .ConstructUsing(c => new AddAccountCommand(c.UserName, c.Password, c.FullName, c.Email, c.PhoneNumber,
                                                       c.UserTypeId, c.Avatar));
        CreateMap<AccountEditViewModel, UpdateAccountCommand>()
            .ConstructUsing(c => new UpdateAccountCommand(c.Email, c.FullName, c.PhoneNumber, c.UserTypeId,
                                                          c.Avatar));

        CreateMap<ModuleGroupViewModel, AddModuleGroupCommand>();
        CreateMap<ModuleGroupViewModel, UpdateModuleGroupCommand>();

    #region Portal

        CreateMap<PortalViewModel, AddPortalCommand>()
            .ConstructUsing(c => new AddPortalCommand(c.Name, c.IsDepartmentOfEducation, c.UnitCode, c.Identifier,
                                                      new AddUserCommand(c.UserViewModel.UserName,
                                                                         c.UserViewModel.Password,
                                                                         c.UserViewModel.FullName,
                                                                         c.UserViewModel.Email,
                                                                         c.UserViewModel.PhoneNumber)));
        CreateMap<PortalEditViewModel, UpdatePortalCommand>()
            .ConstructUsing(c => new UpdatePortalCommand(c.Name));

    #endregion

        CreateMap<AddAccountEmailViewModel, AddEmailAccountCommand>();
        // CreateMap<ReissuePasswordViewModel, ReissuePasswordCommand>();

        //Personal Account
        CreateMap<PersonnelAccountViewModel, AddPersonnelAccountCommand>()
            .ConstructUsing(c => new AddPersonnelAccountCommand(c.Password, c.UserTypeId, c.StaffRecords,
                                                                c.UserName, c.IsAutoGenUserName));
        CreateMap<EditPersonnelAccountViewModel, UpdatePersonnelAccountCommand>()
            .ConstructUsing(c => new UpdatePersonnelAccountCommand(c.Email, c.FullName, c.PhoneNumber, c.UserTypeId,
                                                                   c.Address));

    #endregion Manage Role Identity
    }

#endregion
}