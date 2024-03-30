#region

using AutoMapper;
using ITC.Application.ViewModels.Account;
using ITC.Application.ViewModels.ManageRole;
using ITC.Infra.CrossCutting.Identity.Models;
using ITC.Infra.CrossCutting.Identity.Models.QueryModel;

#endregion

namespace ITC.Application.AutoMapper.ModelToViewModels;

/// <summary>
/// </summary>
public class IdentityMappingProfile : Profile
{
#region Constructors

    public IdentityMappingProfile()
    {
        CreateMap<Module, ModuleViewModel>();
        CreateMap<ModuleGroup, ModuleGroupViewModel>();
        CreateMap<Function, FunctionViewModel>();
        CreateMap<ApplicationUser, AccountViewModel>();
        CreateMap<ApplicationUser, AccountEditViewModel>();
        CreateMap<CustomModuleModel, CustomModuleViewModel>();
        CreateMap<CustomUserTypeModel, CustomerUserTypeViewModel>();

    #region Portal

        CreateMap<Portal, PortalViewModel>();
        CreateMap<Portal, PortalEditViewModel>();

    #endregion
    }

#endregion
}