#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITC.Domain.Commands.SystemManagers.ServerFileManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.NewsManagers.NewsConfigManagers;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.ServerFileManagers;

/// <summary>
///     Class service server-file
/// </summary>
public class ServerFileAppService : IServerFileAppService
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="serverFileQueries"></param>
    /// <param name="serverFileRepository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="configRepository"></param>
    /// <param name="user"></param>
    public ServerFileAppService(IMapper                  mapper,
                                IMediatorHandler         bus,
                                IServerFileQueries       serverFileQueries,
                                IServerFileRepository    serverFileRepository,
                                IAuthorityManagerQueries authorityManagerQueries,
                                INewsConfigRepository    configRepository,
                                IUser                    user)
    {
        _mapper                  = mapper;
        _bus                     = bus;
        _serverFileQueries       = serverFileQueries;
        _serverFileRepository    = serverFileRepository;
        _authorityManagerQueries = authorityManagerQueries;
        _configRepository        = configRepository;
        _user                    = user;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<Guid> UploadFile(UploadFileEventModel model)
    {
        var sendCommand = new UploadServerFileCommand(model);
        await _bus.SendCommand(sendCommand);
        model.Id            = sendCommand.Id;
        model.Name          = sendCommand.FileName;
        model.ResultCommand = sendCommand.ResultCommand;
        return model.Id;
    }

    /// <inheritdoc cref="UploadServerFile"/>
    public async Task<string> UploadServerFile(UploadFileEventModel model)
    {
        var sendCommand = new UploadServerFile2023Command(model);
        await _bus.SendCommand(sendCommand);
        model.Id            = sendCommand.Id;
        model.Name          = sendCommand.FileName;
        model.ResultCommand = sendCommand.ResultCommand;
        model.Link          = sendCommand.Link;
        return model.Link;
    }

    /// <inheritdoc cref="UploadFileAttack" />
    public async Task<SendIdAttackFileModel> UploadFileAttack(UploadFileEventModel model)
    {
        if (model.IsSaveWithManagement == 0)
            model.ManagementId = _user.ManagementId.ToString();
        var sendCommand = new UploadServerFileAttackCommand(model);
        await _bus.SendCommand(sendCommand);
        return new SendIdAttackFileModel
        {
            ResultCommand = sendCommand.ResultCommand,
            Models        = sendCommand.AttackFileIdModels
        };
    }

    /// <inheritdoc cref="UpdateFileName" />
    public bool UpdateFileName(UpdateFileNameModal model)
    {
        var sendCommand = new UpdateFileNameServerFileCommand(model);
        _bus.SendCommand(sendCommand);
        model.ResultCommand = sendCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<ServerFilePagingDto>> GetPaging(string search, Guid workManagerId)
    {
        return await _serverFileQueries.GetPaging(search, workManagerId);
    }

    /// <inheritdoc cref="GetPagingById" />
    public async Task<IEnumerable<ServerFilePagingDto>> GetPagingById(string attackId)
    {
        var listModel = new NCoreHelper().ConvertJsonSerializer<Guid>(attackId);
        return await _serverFileQueries.GetPagingById(listModel);
    }

    /// <inheritdoc cref="Delete" />
    public async Task<bool> Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteServerFileCommand(model.Models);
        await _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="CallDeQuy" />
    public async Task<bool> CallDeQuy()
    {
        var command = new CallDeQuyServerFileCommand();
        await _bus.SendCommand(command);
        return true;
    }

    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(TreeViewPagingModelLibrary model)
    {
        return await Task.Run(() =>
        {
            //===============================Quyền người dùng=======================================================
            var iPermission = _authorityManagerQueries
                              .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                              .Result;
            //======================================================================================================

            var lMenu = _serverFileQueries.GetTreeView(model.VSearch, model.UserId).Result.ToList();
            return new TreeViewHelper().GetTreeViewWithTreeSelectVueJs(lMenu, _user.UserId, iPermission).Result;
        });
    }

    #region ==================================================FOLDER====================================================

    /// <inheritdoc cref="FolderAdd" />
    public bool FolderAdd(FolderServerFileEvent model)
    {
        var evCommand = _mapper.Map<AddFolderServerFileCommand>(model);
        _bus.SendCommand(evCommand);
        model.Id            = evCommand.Id;
        model.ResultCommand = evCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="FolderUpdate" />
    public bool FolderUpdate(FolderServerFileEvent model)
    {
        var evCommand = _mapper.Map<UpdateFolderServerFileCommand>(model);
        _bus.SendCommand(evCommand);
        model.Id            = evCommand.Id;
        model.ResultCommand = evCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="ParentUpdate" />
    public bool ParentUpdate(FolderServerFileEvent model)
    {
        var evCommand = _mapper.Map<UpdateParentServerFileCommand>(model);
        _bus.SendCommand(evCommand);
        model.Id            = evCommand.Id;
        model.ResultCommand = evCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetByFolderId" />
    public FolderServerFileEvent GetByFolderId(Guid id)
    {
        var iReturn = _serverFileRepository.GetAsync(id).Result;
        if (iReturn == null) return null;

        return new FolderServerFileEvent
        {
            Id          = iReturn.Id,
            Name        = iReturn.FileName,
            CreatedBy   = iReturn.CreatedBy,
            Description = iReturn.Description,
            ParentId    = iReturn.ParentId
        };
    }

    #endregion

    #region ==================================================DETAIL====================================================

    /// <inheritdoc cref="GetServerFileDetailPagingDto" />
    public async Task<IEnumerable<ServerFileDetailPagingDto>> GetServerFileDetailPagingDto(
        string vSearch, int pageSize, int pageNumber, string authorId, Guid serverFileId, int groupContentTypeId)
    {
        return await Task.Run(() =>
        {
            var host  = _configRepository.GetByIdIntAsync(1).Result;
            // var vInfo = _serverFileRepository.GetAsync(serverFileId).Result;
            var data = _serverFileQueries.GetServerFileDetailPagingDto(vSearch,
                                                                       pageSize,
                                                                       pageNumber,
                                                                       0,
                                                                       0,
                                                                       authorId,
                                                                       groupContentTypeId).Result;
            foreach (var items in data)
            {
                items.Link = ReturnUrlImage(host?.Host, items.IsLocal, items.FilePath, items.LinkUrl);
            }

            return data;
        });
    }

    private string ReturnUrlImage(string host, bool isLocal, string filePath, string urlLink)
    {
        if (!isLocal) return urlLink;
        
        if (string.IsNullOrEmpty(filePath)) return "";
        var cut       = filePath.Split("\\");
        if (cut.Length == 1) return urlLink;
        var cutExtend = filePath.Split(".");
        return host + "/" + cut[0] + "/" + cut[1] + "/" + cut[2] + "/" + cut[3] + "/320x320" + cutExtend[^1];

    }

    /// <inheritdoc cref="UploadDifference" />
    public async Task<bool> UploadDifference(UploadDifferenceEventModal model)
    {
        var addCommand = _mapper.Map<UploadDifferenceServerFileCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="ViewFile" />
    public async Task<ServerFileDetailInfo> ViewFile(Guid id)
    {
        var vData = await _serverFileRepository.GetAsync(id);
        if (vData == null) return null;

        var vParent = vData.ParentId == null
            ? null
            : await _serverFileRepository.GetByParentId(Guid.Parse(vData.ParentId));
        return new ServerFileDetailInfo
        {
            Id         = vData.Id,
            Name       = vData.FileName,
            LinkUrl    = vData.LinkUrl,
            ParentId   = vData.ParentId,
            IsLocal    = vData.IsLocal,
            FilePath   = vData.FilePath,
            FolderName = vParent?.FileName ?? ""
        };
    }

    /// <inheritdoc cref="UpdateNameFile" />
    public async Task<bool> UpdateNameFile(UpdateFileNameModal model)
    {
        var addCommand = _mapper.Map<UpdateFileNameServerFileCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id            = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="ResizeImageType" />
    public async Task<IEnumerable<ResizeImageDto>> ResizeImageType(ResizeImageModal model)
    {
        return await _serverFileQueries.ResizeImageType(model);
    }

    #endregion

    #region Fields

    private readonly IMediatorHandler         _bus;
    private readonly IServerFileQueries       _serverFileQueries;
    private readonly IServerFileRepository    _serverFileRepository;
    private readonly IAuthorityManagerQueries _authorityManagerQueries;
    private readonly INewsConfigRepository    _configRepository;
    private readonly IUser                    _user;
    private readonly IMapper                  _mapper;

    #endregion
}