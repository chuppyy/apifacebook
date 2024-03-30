#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using ITC.Domain.Models.CompanyManagers;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.CompanyManagers.StaffManagers;

/// <summary>
///     Class service nhân viên
/// </summary>
public class StaffManagerAppService : IStaffManagerAppService
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="staffManagerQueries"></param>
    /// <param name="staffManagerRepository"></param>
    /// <param name="serverFileRepository"></param>
    /// <param name="accountRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="authorityManagerRepository"></param>
    /// <param name="user"></param>
    public StaffManagerAppService(IMapper                     mapper,
                                  IMediatorHandler            bus,
                                  IStaffManagerQueries        staffManagerQueries,
                                  IStaffManagerRepository     staffManagerRepository,
                                  IServerFileRepository       serverFileRepository,
                                  IAccountRepository          accountRepository,
                                  IAuthorityManagerQueries    authorityManagerQueries,
                                  IAuthorityManagerRepository authorityManagerRepository,
                                  IUser                       user)
    {
        _mapper                     = mapper;
        _bus                        = bus;
        _staffManagerQueries        = staffManagerQueries;
        _staffManagerRepository     = staffManagerRepository;
        _serverFileRepository       = serverFileRepository;
        _accountRepository          = accountRepository;
        _authorityManagerQueries    = authorityManagerQueries;
        _authorityManagerRepository = authorityManagerRepository;
        _user                       = user;
    }

    #endregion

    #region ===============================STAFF=============================

    /// <inheritdoc cref="Add" />
    public async Task<bool> Add(StaffManagerEventModel model)
    {
        var addCommand = _mapper.Map<AddStaffManagerCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public async Task<bool> Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteStaffManagerCommand(model.Models);
        await _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public StaffManagerEventModel GetById(Guid id)
    {
        var iReturn = _staffManagerRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        var iAccount = _accountRepository.GetUserByIdAsync(iReturn.UserId).Result;
        return new StaffManagerEventModel
        {
            Id                 = iReturn.Id,
            Name               = iReturn.Name,
            CreatedBy          = iReturn.CreatedBy,
            Description        = iReturn.Description,
            Address            = iReturn.Address,
            Email              = iReturn.Email,
            Phone              = iReturn.Phone,
            RoomManagerId      = iReturn.RoomManagerId,
            UserName           = iAccount.UserName,
            AvatarId           = iReturn.AvatarId,
            AuthorityManagerId = iReturn.AuthorityId,
            UserTypeManagerId  = iReturn.UserTypeManagerId,
            UserCode           = iReturn.UserCode
        };
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(StaffManagerEventModel model)
    {
        var addCommand = _mapper.Map<UpdateStaffManagerCommand>(model);
        await _bus.SendCommand(addCommand);
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <param name="vSearch"></param>
    /// <inheritdoc cref="GetCombobox" />
    public Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        return _staffManagerQueries.GetCombobox(vSearch);
    }

    /// <inheritdoc cref="GetComboboxAuthor" />
    public Task<IEnumerable<ComboboxAuthorModal>> GetComboboxAuthor(
        string vSearch, int pageSize, int pageNumber)
    {
        return _staffManagerQueries.GetComboboxAuthor(vSearch, pageSize, pageNumber);
    }

    /// <inheritdoc cref="GetListChecked" />
    public async Task<IEnumerable<StaffManagerCheckSelectViewModel>> GetListChecked(
        string vSearch, Guid processingStreamId)
    {
        return await _staffManagerQueries.GetListCheckedProcessingStreamStaff(vSearch, processingStreamId);
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<StaffManagerPagingDto>> GetPaging(StaffManagerPagingViewModel model,
                                                                    int                         groupTable)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var iPermission =
                _authorityManagerQueries
                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                    .Result;
            // Xử lý dữ liệu
            if (model.ProjectId.Equals(Guid.Empty)) model.ProjectId = _user.ProjectId;
            var lData = (List<StaffManagerPagingDto>)_staffManagerQueries
                                                     .GetPaging(model, groupTable, _user.ManagementId).Result;
            // Trả về Actions
            foreach (var item in lData)
                item.Actions =
                    ActionPagingWorkManager.ActionAuthoritiesNews(iPermission,
                                                                  item.OwnerId,
                                                                  _user.UserId,
                                                                  false,
                                                                  item.StatusId);
            return lData;
        });
    }

    /// <inheritdoc cref="GetPagingUserTracing" />
    public async Task<IEnumerable<UserTracingPagingDto>> GetPagingUserTracing(PagingModel model)
    {
        return await _staffManagerQueries.GetPagingUserTracing(model);
    }

    /// <inheritdoc cref="AvatarManager" />
    public async Task<bool> AvatarManager(UploadImageStaffEventModel model)
    {
        var rCommand = _mapper.Map<AvatarManagerStaffManagerCommand>(model);
        await _bus.SendCommand(rCommand);
        return model.ResultCommand;
    }

    /// <inheritdoc cref="AvatarLink" />
    public string AvatarLink(string userId)
    {
        var iStaff = _staffManagerRepository.GetByUserId(userId).Result;
        if (iStaff == null) return null;

        var iAvatar = _serverFileRepository.GetAsync(iStaff.AvatarId).Result;
        return iAvatar == null ? "" : iAvatar.AvatarLink;
    }

    /// <inheritdoc cref="GetUrlHomePage" />
    public string GetUrlHomePage(string userId)
    {
        var iStaff = _staffManagerRepository.GetByUserId(userId).Result;
        if (iStaff == null) return null;

        var iAvatar = _authorityManagerRepository.GetAsync(iStaff.AuthorityId).Result;
        return iAvatar == null ? "" : iAvatar.UrlHomePage;
    }

    /// <inheritdoc cref="GetListStaffByProcessingStreamId" />
    public Task<IEnumerable<ComboboxModal>> GetListStaffByProcessingStreamId(
        string vSearch, Guid processingStreamId)
    {
        return _staffManagerQueries.GetListStaffByProcessingStreamId(vSearch, processingStreamId);
    }

    /// <inheritdoc cref="GetByUserId" />
    public async Task<StaffManager> GetByUserId(string userId)
    {
        return await _staffManagerRepository.GetByUserId(userId);
    }

    /// <inheritdoc cref="GetStaffInSystem" />
    public async Task<IEnumerable<ComboboxModal>> GetStaffInSystem()
    {
        return await _staffManagerQueries.GetStaffInSystem();
    }

    /// <inheritdoc cref="GetByUserId2" />
    public async Task<StaffManagerByUserDto> GetByUserId2(string userId)
    {
        return await _staffManagerQueries.GetByUserId2(userId);
    }

    #endregion

    #region Fields

    private readonly IMediatorHandler            _bus;
    private readonly IStaffManagerQueries        _staffManagerQueries;
    private readonly IStaffManagerRepository     _staffManagerRepository;
    private readonly IServerFileRepository       _serverFileRepository;
    private readonly IAccountRepository          _accountRepository;
    private readonly IAuthorityManagerQueries    _authorityManagerQueries;
    private readonly IAuthorityManagerRepository _authorityManagerRepository;
    private readonly IUser                       _user;
    private readonly IMapper                     _mapper;

    #endregion
}