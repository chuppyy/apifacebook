#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Class service danh mục
/// </summary>
public class MenuManagerAppService : IMenuManagerAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="queries"></param>
    /// <param name="repository"></param>
    /// <param name="staffManagerRepository"></param>
    /// <param name="authorityDetailRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="user"></param>
    /// <param name="bus"></param>
    public MenuManagerAppService(IMapper                    mapper,
                                 IMenuManagerQueries        queries,
                                 IMenuManagerRepository     repository,
                                 IStaffManagerRepository    staffManagerRepository,
                                 IAuthorityDetailRepository authorityDetailRepository,
                                 IAuthorityManagerQueries   authorityManagerQueries,
                                 IUser                      user,
                                 IMediatorHandler           bus)
    {
        _mapper                    = mapper;
        _queries                   = queries;
        _repository                = repository;
        _staffManagerRepository    = staffManagerRepository;
        _authorityDetailRepository = authorityDetailRepository;
        _authorityManagerQueries   = authorityManagerQueries;
        _user                      = user;
        _bus                       = bus;
    }

#endregion

#region Fields

    private readonly IMediatorHandler           _bus;
    private readonly IMapper                    _mapper;
    private readonly IMenuManagerQueries        _queries;
    private readonly IMenuManagerRepository     _repository;
    private readonly IStaffManagerRepository    _staffManagerRepository;
    private readonly IAuthorityDetailRepository _authorityDetailRepository;
    private readonly IAuthorityManagerQueries   _authorityManagerQueries;
    private readonly IUser                      _user;

#endregion

#region IMenuManagerAppService Members

    /// <summary>
    ///     Thêm danh mục
    /// </summary>
    /// <param name="model"></param>
    public void Add(MenuManagerEventModel model)
    {
        var addCommand = _mapper.Map<AddMenuManagerCommand>(model);
        _bus.SendCommand(addCommand);
        model.Id = addCommand.Id;
    }

    /// <summary>
    ///     Xóa danh mục
    /// </summary>
    /// <param name="model"></param>
    public void Delete(Guid model)
    {
        var deleteCommand = new DeleteMenuManagerCommand(model);
        _bus.SendCommand(deleteCommand);
    }

    /// <inheritdoc cref="DeleteAuthorities" />
    public void DeleteAuthorities(string model, Guid projectId, Guid companyId)
    {
        var lSend = new NCoreHelper().ConvertJsonSerializer<Guid>(model);
        _bus.SendCommand(new DeleteAuthoritiesMenuManagerCommand(lSend)
        {
            ProjectId = projectId,
            CompanyId = companyId
        });
    }

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MenuManagerEventModel GetById(Guid id)
    {
        var iReturn = _repository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new MenuManagerEventModel
        {
            Code            = iReturn.Code,
            Id              = iReturn.Id,
            Name            = iReturn.Name,
            ParentId        = iReturn.ParentId,
            Label           = "",
            Position        = iReturn.Position,
            Router          = iReturn.Router,
            CreatedBy       = iReturn.CreatedBy,
            ManagementId    = "",
            MLeft           = iReturn.MLeft,
            MRight          = iReturn.MRight,
            PermissionValue = iReturn.PermissionValue,
            ProjectId       = iReturn.ProjectId,
            ManagerICon     = iReturn.ManagerICon,
            MenuGroupId     = iReturn.MenuGroupId
        };
    }

    /// <param name="version"></param>
    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(int version)
    {
        return await Task.Run(() =>
        {
            var lMenu = _queries.GetTreeView(version).Result.ToList();
            // Nhóm dữ liệu
            var lGroup = lMenu.Where(x => x.ParentId == Guid.Empty.ToString()).ToList();

            return lGroup.Select(zViewModel => new TreeViewProjectModel
                         {
                             Icon            = zViewModel.Icon,
                             Id              = zViewModel.Id,
                             ParentId        = zViewModel.ParentId,
                             Opened          = false,
                             Text            = zViewModel.Text,
                             Label           = zViewModel.Text,
                             OpenedAction    = true,
                             PermissionValue = zViewModel.PermissionValue,
                             MenuGroupId     = zViewModel.MenuGroupId,
                             Position        = zViewModel.Position,
                             Children        = ReturnChilderen(zViewModel.Id, lMenu)
                         })
                         .ToList();
        });
    }

    /// <param name="value"></param>
    /// <inheritdoc cref="GetPermissionDefault" />
    public async Task<IEnumerable<PermissionDefaultViewModal>> GetPermissionDefault(int value)
    {
        var lData = (List<PermissionDefaultViewModal>)await _queries.GetPermissionDefault();
        foreach (var zViewModal in lData)
            zViewModal.Checked = (zViewModal.Value & value) != 0 ? "accepted" : "not_accepted";

        return lData;
    }

    /// <inheritdoc cref="V2023GetPermissionDefault" />
    public async Task<IEnumerable<v2023PermissionDefaultViewModal>> V2023GetPermissionDefault(int value)
    {
        var lData = await _queries.GetPermissionDefault();
        return lData.Select(zViewModal => new v2023PermissionDefaultViewModal
        {
            Checked = (zViewModal.Value & value) != 0,
            Id      = zViewModal.Id,
            Name    = zViewModal.Name,
            Value   = zViewModal.Value
        }).ToList();
    }

    /// <inheritdoc cref="GetMenuByAuthorities" />
    public async Task<IEnumerable<MenuRoleEventViewModel>> GetMenuByAuthorities(Guid authority, bool isAdmin)
    {
        return await Task.Run(() =>
        {
            var isAddNewInProject = false;
            if (isAdmin == false)
            {
                if (authority.CompareTo(Guid.Empty) == 0) isAddNewInProject = true;

                // Không phải tài khoản quản trị toàn hệ thống, gán lại Authority
                Guid.TryParse(_user.StaffId, out var iStaffId);
                var iStaffInfo = _staffManagerRepository.GetAsync(iStaffId).Result;
                if (iStaffInfo == null) return null;

                authority = iStaffInfo.AuthorityId;
            }

            var isAddNew =
                string.Compare(authority.ToString(), Guid.Empty.ToString(),
                               StringComparison.Ordinal) == 0;

            var lMenu =
                _queries.GetMenuByAuthorities(authority, isAdmin, isAddNewInProject)
                        .Result
                        .OrderBy(x => x.MenuGroupId)
                        .ThenBy(x => x.Position)
                        .ToList();
            var lGroup      = lMenu.Where(x => x.ParentId == Guid.Empty.ToString()).ToList();
            var lPermission = _queries.GetPermissionDefault().Result.ToList();
            return lGroup.Select(zViewModel => new MenuRoleEventViewModel
                         {
                             Id       = zViewModel.Id,
                             Opened   = false,
                             Text     = zViewModel.Name,
                             Selected = !isAddNew && (zViewModel.PermissionValue & 1) != 0,
                             ParentCount =
                                 lMenu.Count(x => x.ParentId == zViewModel.Id.ToString()),
                             Children = ReturnChilderenMenuByAuthorities(zViewModel.Id,
                                                                         zViewModel, isAddNew, lMenu,
                                                                         lPermission)
                         })
                         .ToList();
        });
    }

    /// <inheritdoc cref="AddAuthorities" />
    public void AddAuthorities(AuthoritiesMenuManagerEventModel model)
    {
        var addCommand = _mapper.Map<AddAuthoritiesMenuManagerCommand>(model);
        _bus.SendCommand(addCommand);
    }

    /// <inheritdoc cref="UpdateAuthorities" />
    public void UpdateAuthorities(AuthoritiesMenuManagerEventModel model)
    {
        var addCommand = _mapper.Map<UpdateAuthoritiesMenuManagerCommand>(model);
        _bus.SendCommand(addCommand);
    }

    /// <inheritdoc cref="GetAuthoritiesAsync" />
    public Task<IEnumerable<AuthoritiesViewModel>> GetAuthoritiesAsync(
        Guid companyId, Guid projectId, string search, int pageSize, int pageNumber)
    {
        return _queries.GetAuthoritiesAsync(companyId, projectId, search, pageSize, pageNumber);
    }

    /// <inheritdoc cref="GetAuthoritiesCombobox" />
    public Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesCombobox(Guid companyId, Guid projectId)
    {
        return _queries.GetAuthoritiesCombobox(companyId, projectId);
    }

    /// <inheritdoc cref="GetAuthoritiesById" />
    public Task<IEnumerable<KeyValuePair<Guid, string>>> GetAuthoritiesById(Guid companyId)
    {
        return _queries.GetAuthoritiesById(companyId);
    }

    /// <inheritdoc cref="GetMenu" />
    public async Task<IEnumerable<MenuRoleReturnViewModel>> GetMenu(string userId)
    {
        return await Task.Run(() =>
        {
            var lMenu = _queries.GetMenu(userId).Result.ToList();
            // Nhóm dữ liệu
            var lGroup =
                lMenu.Where(x => x.ParentId == Guid.Empty.ToString())
                     .OrderBy(x => x.Position);

            return (from iGroup in lGroup
                let rChild = ReturnMenuChildren(iGroup.Code, lMenu)
                select new MenuRoleReturnViewModel
                {
                    Icon = iGroup.Icon,
                    Id = iGroup.Id,
                    Label = iGroup.Label, //zViewModel.MenuGroupId + ". " + zViewModel.Label,
                    Position = iGroup.Position,
                    Subs = rChild,
                    Children = rChild,
                    To = iGroup.To,
                    ParentId = iGroup.ParentId,
                    MenuGroupId = iGroup.MenuGroupId
                }).ToList();
        });
    }

    /// <inheritdoc cref="GetPermissionByAuthorities" />
    public async Task<IEnumerable<PermissionByAuthoritiesModel>> GetPermissionByAuthorities(Guid authoritiesId)
    {
        return await Task.Run(() =>
        {
            var lReturn = new List<PermissionByAuthoritiesModel>();
            // Danh sách quyền mặc định của hệ thống
            var lPermission = (List<PermissionDefaultViewModal>)_queries.GetPermissionDefault().Result;
            // Dữ liệu quyền chi tiết
            var iInfo = _authorityDetailRepository.GetAsync(authoritiesId).Result;
            // Danh sách các quyền được cấp cho chức năng này
            var iMenuManagerInfo = _repository.GetAsync(iInfo.MenuManagerId).Result;
            // Gán dữ liệu
            foreach (var iPermission in lPermission)
                if ((iPermission.Value & iMenuManagerInfo.PermissionValue) != 0)
                {
                    var iAccept = 0;
                    // Kiểm tra quyền này so với quyền đã cấp là đã được cấp hay chưa ?
                    if ((iPermission.Value & iInfo.Value) != 0)
                        // Đã cấp
                        iAccept = 1;

                    lReturn.Add(new PermissionByAuthoritiesModel
                    {
                        Checked             = iAccept,
                        Name                = iPermission.Name,
                        Value               = iPermission.Value,
                        AuthoritiesDetailId = authoritiesId
                    });
                }

            return lReturn;
        });
    }

    /// <inheritdoc cref="UpdatePermissionByAuthorities" />
    public async Task<bool> UpdatePermissionByAuthorities(SortMenuPermissionByAuthoritiesModel model)
    {
        var rCommand = _mapper.Map<UpdatePermissionByAuthoritiesCommand>(model);
        await _bus.SendCommand(rCommand);
        return rCommand.ResultCommand;
    }

    /// <inheritdoc cref="V2023GetMenu" />
    public async Task<IEnumerable<v3MenuReturnFeModel>> V2023GetMenu(string userId)
    {
        // {
        //     icon: "HomeIcon",
        //     pageName: "home-manager-overview",
        //     title: "1. Trang chủ",
        // },
        // {
        //     icon: "SettingsIcon",
        //     pageName: "side-menu-components",
        //     title: "7. Hệ thống",
        //     subMenu: [
        //     {
        //         icon: "",
        //         pageName: "side-menu-table",
        //         title: "1. Danh mục",
        //         subMenu: [
        //         {
        //             icon: "",
        //             pageName: "position-manager",
        //             title: "1. Chức vụ",
        //         },
        //         ],
        //     },
        //     ],
        // },
        return await Task.Run(() =>
        {
            var lMenu = _queries.v2023GetMenu(userId, 2023).Result.ToList();
            // Nhóm dữ liệu
            var lGroup =
                lMenu.Where(x => x.ParentId == Guid.Empty.ToString())
                     .OrderBy(x => x.Position)
                     .ToList();

            return (from zViewModel in lGroup
                    let result = ReturnMenuChildrenV2023(zViewModel.Code, lMenu)
                    select new v3MenuReturnFeModel
                    {
                        Icon     = zViewModel.Icon,
                        Title    = zViewModel.Label,
                        PageName = zViewModel.To,
                        SubMenu  = result
                    }).ToList();
        });
    }

    /// <inheritdoc cref="V2023GetMenuVersion" />
    public async Task<IEnumerable<ComboboxModalInt>> V2023GetMenuVersion()
    {
        return await Task.Run(() =>
        {
            var lData = _queries.v2023GetMenuVersion().Result.ToList();
            return lData.Select(items => new ComboboxModalInt
                        {
                            Id   = items,
                            Name = items.ToString()
                        })
                        .ToList();
        });
    }

    /// <inheritdoc cref="V202301GetMenuByAuthorities" />
    public async Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetMenuByAuthorities(Guid authority)
    {
        return await Task.Run(async () =>
        {
            var lData = await _queries.V202301GetMenuByAuthorities(authority);
            var menuByAuthoritiesV2023S = lData.ToList();
            var lGroup = menuByAuthoritiesV2023S.Where(x => x.MenuParent == Guid.Empty.ToString()).ToList();

            return lGroup.Select(items => new MenuByAuthoritiesV2023
                         {
                             Id         = items.Id,
                             Label      = items.Label,
                             MenuParent = items.MenuParent,
                             MenuName   = items.MenuName,
                             Position   = items.Position,
                             Nodes      = V202301GetMenuByAuthoritiesChild(items.MenuId, menuByAuthoritiesV2023S)
                         })
                         .ToList();
        });
    }

    /// <inheritdoc cref="V202301GetFeatureByAuthoritiesParent" />
    public async Task<IEnumerable<MenuByAuthoritiesV2023>> V202301GetFeatureByAuthoritiesParent(
        Guid authority, Guid menuId)
    {
        var position = 1;
        var lData =
            (await _queries.V202301GetMenuByAuthorities(authority)).Where(x => x.MenuParent == menuId.ToString())
                                                                   .OrderBy(x => x.Position);
        return lData.Select(items => new MenuByAuthoritiesV2023
                    {
                        Id         = items.Id,
                        Label      = items.Label,
                        Nodes      = null,
                        Position   = position++,
                        Value      = items.Value,
                        MenuId     = items.MenuId,
                        MenuName   = items.MenuName,
                        MenuParent = items.MenuParent
                    })
                    .ToList();
    }

    /// <inheritdoc cref="V202301GetPermissionDefaultByMenu" />
    public async Task<IEnumerable<v2023PermissionDefaultViewModal>> V202301GetPermissionDefaultByMenu(
        Guid authority, Guid menuId, int data)
    {
        return await Task.Run(async () =>
        {
            var iData =
                (await _queries.V202301GetMenuDefault(menuId)).FirstOrDefault();
            var permissionDefault = await _queries.GetPermissionDefault();

            return (from items in permissionDefault
                    where iData?.Value > 0 && (items.Value & iData.Value) > 0
                    select new v2023PermissionDefaultViewModal
                    {
                        Checked = (data & items.Value) > 0,
                        Name    = items.Name,
                        Value   = items.Value,
                        Id      = items.Id
                    }).ToList();
        });
    }

    /// <summary>
    ///     Trả về menu con của MenuByAuthoritiesV2023
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fatherData"></param>
    /// <returns></returns>
    private List<MenuByAuthoritiesV2023> V202301GetMenuByAuthoritiesChild(Guid                         id,
                                                                          List<MenuByAuthoritiesV2023> fatherData)
    {
        if (fatherData == null) return null;

        var lData = fatherData.Where(x => x.MenuParent.ToLower() == id.ToString().ToLower())
                              .OrderBy(x => x.Position)
                              .ToList();
        if (!lData.Any()) return null;

        foreach (var items in lData) items.Nodes = V202301GetMenuByAuthoritiesChild(items.MenuId, fatherData);

        return lData;
    }

    /// <summary>
    ///     Trả về dữ liệu menu con theo dữ liệu menu cha
    /// </summary>
    /// <param name="id"></param>
    /// <param name="lFather"></param>
    /// <returns></returns>
    private IEnumerable<MenuRoleReturnViewModel> ReturnMenuChildren(Guid id, IEnumerable<MenuRoleReturnViewModel> lFather)
    {
        if (lFather == null) return null;
        IEnumerable<MenuRoleReturnViewModel> lData =
            lFather.Where(x => x.ParentId.ToLower() == id.ToString().ToLower())
                   .OrderBy(x => x.Position);
        if (!lData.Any()) return null;
        foreach (var items in lData)
            if ((items.Value & 1) != 0)
            {
                var rResult = ReturnMenuChildren(items.Code, lFather);
                items.Subs     = rResult;
                items.Children = rResult;
            }

        return lData;
    }

    /// <summary>
    ///     [v2023] Trả về dữ liệu menu cha - con
    /// </summary>
    /// <param name="id"></param>
    /// <param name="lFather"></param>
    /// <returns></returns>
    private List<v3MenuReturnFeModel> ReturnMenuChildrenV2023(Guid id, List<v3MenuReturnFeModel> lFather)
    {
        List<v3MenuReturnFeModel> lData;
        var                       iReturnNull = false;
        if (lFather != null)
        {
            lData = lFather
                    .Where(x => string.Equals(x.ParentId, id.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(x => x.Position)
                    .ToList();
            if (!lData.Any()) return null;

            foreach (var items in lData)
                if ((items.Value & 1) != 0)
                {
                    items.Icon     = items.Icon;
                    items.Title    = items.Label;
                    items.PageName = items.To;
                    items.SubMenu  = ReturnMenuChildrenV2023(items.Code, lFather);
                }
                else
                {
                    iReturnNull = true;
                }

            return iReturnNull ? null : lData;
        }

        return null;
    }

    private List<TreeViewProjectModel> ReturnChilderen(Guid id, List<TreeViewProjectModel> lFather)
    {
        if (lFather != null)
        {
            var lData = lFather.Where(x => x.ParentId.ToLower() == id.ToString().ToLower()).ToList();
            if (lData.Any())
            {
                foreach (var items in lData)
                {
                    items.Children        = ReturnChilderen(items.Id, lFather);
                    items.Opened          = false;
                    items.OpenedAction    = true;
                    items.Label           = items.Text;
                    items.PermissionValue = items.PermissionValue;
                    items.MenuGroupId     = items.MenuGroupId;
                    items.Position        = items.Position;
                }

                return lData;
            }

            return null;
        }

        return null;
    }

    private List<MenuRoleEventViewModel> ReturnChilderenMenuByAuthorities(Guid                             id,
                                                                          MenuByAuthoritiesViewModel       viewModel,
                                                                          bool                             isAddNew,
                                                                          List<MenuByAuthoritiesViewModel> lFather,
                                                                          List<PermissionDefaultViewModal> lPermission)
    {
        if (lFather != null)
        {
            var lReturn = new List<MenuRoleEventViewModel>();
            var lData   = lFather.Where(x => x.ParentId.ToLower() == id.ToString().ToLower()).ToList();
            if (lData.Any())
            {
                // Có dữ liệu menu con
                lReturn.AddRange(lData.Select(items => new MenuRoleEventViewModel
                {
                    Id          = items.Id,
                    ParentId    = id,
                    Opened      = false,
                    Text        = items.Name,
                    Value       = items.PermissionValue,
                    ParentCount = lFather.Count(x => x.ParentId == items.Id.ToString()),
                    Selected    = !isAddNew && (items.PermissionValue & viewModel.PermissionValue) != 0,
                    Children    = ReturnChilderenMenuByAuthorities(items.Id, items, isAddNew, lFather, lPermission)
                }));

                return lReturn;
            }

            // => Không có dữ liệu con => truyền giá trị permission
            var lRefPermission = new List<MenuRoleEventViewModel>();
            foreach (var zPermission in lPermission)
                //Nếu quyền này được phép sử dụng trong nhóm quyền thì cho hiển thị
                if ((zPermission.Value & viewModel.PermissionValueDefault) != 0)
                    lRefPermission.Add(new MenuRoleEventViewModel
                    {
                        Id       = Guid.NewGuid(),
                        ParentId = id,
                        Opened   = false,
                        Text     = zPermission.Name,
                        Value    = zPermission.Value,
                        Children = null,
                        Selected = !isAddNew && (zPermission.Value & viewModel.PermissionValue) != 0
                    });
                else
                    lRefPermission.Add(new MenuRoleEventViewModel
                    {
                        Id       = Guid.NewGuid(),
                        ParentId = id,
                        Opened   = false,
                        Text     = zPermission.Name,
                        Value    = zPermission.Value,
                        Children = null,
                        Selected = false
                    });

            return lRefPermission;
        }

        return null;
    }

    /// <summary>
    ///     Cập nhật danh mục
    /// </summary>
    /// <param name="model"></param>
    public void Update(MenuManagerEventModel model)
    {
        var updateCommand = _mapper.Map<UpdateMenuManagerCommand>(model);
        _bus.SendCommand(updateCommand);
    }

#endregion
}