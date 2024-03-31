#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryManager;
using ITC.Domain.Core.NCoreLocal.Enum;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using NCore.Actions;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SaleProductManagers.ImageLibraryManager;

/// <summary>
///     Class service slide
/// </summary>
public class ImageLibraryManagerAppService : IImageLibraryManagerAppService
{
#region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="imageLibraryManagerQueries"></param>
    /// <param name="imageLibraryManagerRepository"></param>
    /// <param name="imageLibraryDetailManagerRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="serverFileRepository"></param>
    /// <param name="user"></param>
    public ImageLibraryManagerAppService(IMapper                              mapper,
                                         IMediatorHandler                     bus,
                                         IImageLibraryManagerQueries          imageLibraryManagerQueries,
                                         IImageLibraryManagerRepository       imageLibraryManagerRepository,
                                         IImageLibraryDetailManagerRepository imageLibraryDetailManagerRepository,
                                         IAuthorityManagerQueries             authorityManagerQueries,
                                         IServerFileRepository                serverFileRepository,
                                         IUser                                user)
    {
        _mapper                              = mapper;
        _bus                                 = bus;
        _imageLibraryManagerQueries          = imageLibraryManagerQueries;
        _imageLibraryManagerRepository       = imageLibraryManagerRepository;
        _imageLibraryDetailManagerRepository = imageLibraryDetailManagerRepository;
        _authorityManagerQueries             = authorityManagerQueries;
        _serverFileRepository                = serverFileRepository;
        _user                                = user;
    }

#endregion

    /// <inheritdoc cref="Add" />
    public async Task<bool> Add(ImageLibraryManagerEventModel model)
    {
        // var addCommand = _mapper.Map<AddImageLibraryManagerCommand>(model);
        // await _bus.SendCommand(addCommand);
        // model.Id            = addCommand.Id;
        // model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="Delete" />
    public async Task<bool> Delete(DeleteModal model)
    {
        // var deleteCommand = new DeleteImageLibraryManagerCommand(model.Models);
        // await _bus.SendCommand(deleteCommand);
        // model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetById" />
    public ImageLibraryManagerGetByIdModel GetById(Guid id)
    {
        var iReturn = _imageLibraryManagerRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        var iServerFileInfo = iReturn.AvatarId.CompareTo(Guid.Empty) == 0
                                  ? null
                                  : _serverFileRepository.GetAsync(iReturn.AvatarId).Result;

        return new ImageLibraryManagerGetByIdModel
        {
            Id       = iReturn.Id,
            Name     = iReturn.Name,
            Content  = iReturn.Content,
            AvatarId = iServerFileInfo == null ? Guid.Empty : iServerFileInfo.Id,
            IsLocal  = iServerFileInfo?.IsLocal ?? false
        };
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(ImageLibraryManagerEventModel model)
    {
        // var addCommand = _mapper.Map<UpdateImageLibraryManagerCommand>(model);
        // await _bus.SendCommand(addCommand);
        // model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<ImageLibraryManagerPagingDto>> GetPaging(PagingModel model)
    {
        return await Task.Run(() =>
        {
            var iUserCreated = _user.UserId;
            // Lấy dữ liệu quyền của người dùng
            var iPermission =
                _authorityManagerQueries
                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, iUserCreated)
                    .Result;
            // Xử lý dữ liệu
            model.ProjectId = _user.ProjectId;
            var lData =
                (List<ImageLibraryManagerPagingDto>)_imageLibraryManagerQueries.GetPaging(model).Result;
            // Trả về Actions
            foreach (var item in lData)
                item.Actions =
                    ActionPagingWorkManager.ActionAuthoritiesNews(iPermission,
                                                                  item.OwnerId,
                                                                  iUserCreated,
                                                                  false,
                                                                  item.StatusId);
            return lData;
        });
    }

    /// <inheritdoc cref="GetPagingDetail" />
    public async Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetPagingDetail(
        ImageLibraryDetailPagingModel model)
    {
        model.ProjectId = _user.ProjectId;
        return await _imageLibraryManagerQueries.GetPagingDetail(model);
    }

    /// <inheritdoc cref="GetSlideTypeView" />
    public async Task<IEnumerable<ComboboxModalInt>> GetSlideTypeView()
    {
        return await Task.Run(() =>
        {
            var lData = SlideTypeViewEnum.GetList().ToList();
            return lData.Select(items => new ComboboxModalInt { Id = items.Id, Name = items.Name }).ToList();
        });
    }

    /// <inheritdoc cref="AddDetail" />
    public async Task<bool> AddDetail(ImageLibraryDetailManagerEventModel model)
    {
        // var addCommand = _mapper.Map<AddImageLibraryDetailManagerCommand>(model);
        // await _bus.SendCommand(addCommand);
        // model.ResultCommand = addCommand.ResultCommand;
        // model.Id            = addCommand.Id;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="UpdateDetail" />
    public async Task<bool> UpdateDetail(ImageLibraryDetailManagerEventModel model)
    {
        // var addCommand = _mapper.Map<UpdateImageLibraryDetailManagerCommand>(model);
        // await _bus.SendCommand(addCommand);
        // model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="DeleteDetail" />
    public async Task<bool> DeleteDetail(DeleteModal model)
    {
        // var addCommand = _mapper.Map<DeleteImageLibraryDetailManagerCommand>(model);
        // await _bus.SendCommand(addCommand);
        // model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetByIdDetail" />
    public ImageLibraryDetailManagerGetByIdModel GetByIdDetail(Guid id)
    {
        var iReturn = _imageLibraryDetailManagerRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        var iServerFileInfo = iReturn.AvatarId.CompareTo(Guid.Empty) == 0
                                  ? null
                                  : _serverFileRepository.GetAsync(iReturn.AvatarId).Result;

        return new ImageLibraryDetailManagerGetByIdModel
        {
            Id       = iReturn.Id,
            Name     = iReturn.Name,
            Content  = iReturn.Content,
            AvatarId = iServerFileInfo == null ? Guid.Empty : iServerFileInfo.Id,
            IsLocal  = iServerFileInfo?.IsLocal ?? false
        };
    }

    /// <inheritdoc cref="GetBySecrectKey" />
    public async Task<Domain.Models.SaleProductManagers.ImageLibraryManager> GetBySecrectKey(
        Guid projectId, string secrectKey)
    {
        return await _imageLibraryManagerRepository.GetBySecrectKey(projectId, secrectKey);
    }

    /// <inheritdoc cref="GetHomeDetail" />
    public async Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetHomeDetail(
        ImageLibraryDetailPagingModel model, Guid imageId)
    {
        return await Task.Run(() =>
        {
            model.ImageId = imageId;
            return _imageLibraryManagerQueries.GetPagingDetail(model);
        });
    }
    #region Fields

    private readonly IMediatorHandler                     _bus;
    private readonly IImageLibraryManagerQueries          _imageLibraryManagerQueries;
    private readonly IImageLibraryManagerRepository       _imageLibraryManagerRepository;
    private readonly IImageLibraryDetailManagerRepository _imageLibraryDetailManagerRepository;
    private readonly IAuthorityManagerQueries             _authorityManagerQueries;
    private readonly IServerFileRepository                _serverFileRepository;
    private readonly IUser                                _user;
    private readonly IMapper                              _mapper;

#endregion
}