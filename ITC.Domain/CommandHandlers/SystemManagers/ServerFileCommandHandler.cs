using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.SystemManagers.ServerFileManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.SystemManagers;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using NCore.Modals;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.SystemManagers;

/// <summary>
///     Command Handler Server File
/// </summary>
public class ServerFileCommandHandler : CommandHandler,
                                        IRequestHandler<UploadServerFileCommand, bool>,
                                        IRequestHandler<UploadServerFileAttackCommand, bool>,
                                        IRequestHandler<DeleteServerFileCommand, bool>,
                                        IRequestHandler<UpdateFileNameServerFileCommand, bool>,
                                        IRequestHandler<AddFolderServerFileCommand, bool>,
                                        IRequestHandler<UpdateFolderServerFileCommand, bool>,
                                        IRequestHandler<UploadDifferenceServerFileCommand, bool>,
                                        IRequestHandler<UpdateParentServerFileCommand, bool>,
                                        IRequestHandler<CallDeQuyServerFileCommand, bool>,
                                        IRequestHandler<UploadServerFile2023Command, bool>
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="serverFileQueries"></param>
    /// <param name="serverFileRepository"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public ServerFileCommandHandler(IUser                                    user,
                                    IServerFileQueries                       serverFileQueries,
                                    IServerFileRepository                    serverFileRepository,
                                    ISystemLogRepository                     systemLogRepository,
                                    IUnitOfWork                              uow,
                                    IMediatorHandler                         bus,
                                    INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                 = user;
        _serverFileQueries    = serverFileQueries;
        _serverFileRepository = serverFileRepository;
        _systemLogRepository  = systemLogRepository;
    }

    #endregion

    #region IRequestHandler<CallDeQuyServerFileCommand,bool> Members

    /// <summary>
    ///     Handle call đệ quy
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(CallDeQuyServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = true;
        CallDeQuy();
        return await Task.FromResult(true);
    }

    #endregion

    #region IRequestHandler<DeleteServerFileCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteServerFileCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _serverFileQueries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
            command.ResultCommand = true;
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Deleted.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              Guid.Empty,
                                                              "",
                                                              JsonConvert.SerializeObject(command.Model),
                                                              _user.UserId));
            _systemLogRepository.SaveChanges();
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region IRequestHandler<UpdateFileNameServerFileCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật fileName
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateFileNameServerFileCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iValue = _serverFileRepository.GetAsync(command.Id).Result;
        if (iValue != null)
        {
            // Cập nhật dữ liệu
            iValue.FileName = command.FileName;
            iValue.ParentId = command.ParentId;
            _serverFileRepository.Update(iValue);
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              iValue.Id,
                                                              JsonConvert.SerializeObject(iValue),
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }

            NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
            return await Task.FromResult(false);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region =====================================DETAIL====================================

    /// <summary>
    ///     Handle thêm mới từ link khác
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UploadDifferenceServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //=============================================Lấy Extension================================================
        //var cut = command.Link.Split('.');
        //==========================================================================================================

        var iUserId = _user.UserId;
        var iRoot   = await _serverFileRepository.GetDataRoot();
        command.ParentId = string.Compare(command.ParentId, Guid.Empty.ToString(), StringComparison.Ordinal) == 0
            ? iRoot.Id.ToString()
            : command.ParentId;
        if (string.Compare(command.ParentId, Guid.Empty.ToString(), StringComparison.Ordinal) == 0)
        {
            var serverFile = await _serverFileRepository.GetDataRoot();
            command.ParentId = serverFile?.Id.ToString();
        }

        var iKey      = Guid.NewGuid();
        var iPosition = await _serverFileRepository.GetMaxPosition(iUserId);
        var rAdd = new ServerFile(iKey,
                                  command.Name,
                                  command.Description,
                                  command.ParentId,
                                  iPosition,
                                  command.Link,
                                  "",
                                  command.FileType,
                                  command.VideoType,
                                  iUserId);
        await _serverFileRepository.AddAsync(rAdd);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.AddNew.Id,
                                                          _user.StaffId,
                                                          _user.StaffName,
                                                          GetType().Name,
                                                          "Thêm mới dữ liệu: " + command.Name,
                                                          iKey,
                                                          "",
                                                          JsonConvert.SerializeObject(command),
                                                          _user.UserId));
        //==========================================
        if (Commit())
        {
            CallDeQuy();
            command.Id            = iKey;
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region IRequestHandler<UploadServerFileAttackCommand,bool> Members

    /// <summary>
    ///     Handle upload file attack
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UploadServerFileAttackCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        Guid iManagement;
        if (command.IsSaveWithManagement == 1)
            iManagement = string.IsNullOrEmpty(command.ManagementId)
                ? _user.ManagementId
                : Guid.Parse(command.ManagementId);
        else
            iManagement = _user.ManagementId;
        //====================================Lấy ParentId====================================
        var serverFile = await _serverFileRepository.GetDataRoot();
        command.ParentId = serverFile?.Id.ToString();
        //==========================================================================================================

        var createUserId = _user.UserId;
        var folderUserId = string.IsNullOrEmpty(command.ManagementId)
            ? _user.StaffId
            : command.ManagementId;
        var pathFolder  = "wwwroot\\Uploads\\FileFolder\\" + folderUserId + "\\";
        var pathFolder2 = "Uploads\\FileFolder\\"          + folderUserId + "\\";

        var nCore      = new NCoreHelper();
        var iPosition  = await _serverFileRepository.GetMaxPosition(createUserId);
        var lMultiFile = new List<Guid>();
        foreach (var iFile in command.FileModels)
        {
            var extension = "."                + iFile.FileName.Split('.')[iFile.FileName.Split('.').Length - 1].ToLower();
            var fileName  = DateTime.Now.Ticks + extension;
            var iFolder   = nCore.CreateFolder(pathFolder).Result;
            var path      = Path.Combine(Directory.GetCurrentDirectory(), pathFolder + iFolder, fileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await iFile.CopyToAsync(stream, cancellationToken);
            }

            var iKey = Guid.NewGuid();
            var addSaveFile = new ServerFileSendSave
            {
                Id            = iKey,
                Description   = "",
                Folder        = iFolder,
                Position      = iPosition++,
                FileExtension = extension,
                FileName      = string.IsNullOrEmpty(command.Name) ? iFile.FileName : command.Name,
                FileNameRoot  = fileName,
                FilePath      = pathFolder2 + iFolder + "\\" + fileName,
                FileSize      = Convert.ToDouble(iFile.Length),
                FileType      = command.FileType,
                ParentId      = command.ParentId,
                StatusId      = ActionStatusEnum.Active.Id,
                UserId        = createUserId,
                GroupFile     = command.GroupFile,
                ManagementId  = iManagement
            };

            #region ===============================================Nén file======================================

            switch (extension)
            {
                case ".pdf":
                {
                    #region PDF

                    await nCore.Pdf_Resize(path,
                                           Path.Combine(Directory.GetCurrentDirectory(),
                                                        pathFolder     + iFolder,
                                                        iFile.FileName + "" + extension));

                    #endregion

                    break;
                }
            }

            #endregion

            var iResult = await _serverFileQueries.AddFile(addSaveFile);
            if (iResult > 0) lMultiFile.Add(addSaveFile.Id);
        }

        if (lMultiFile.Count > 0)
        {
            command.AttackFileIdModels = lMultiFile;
            command.ResultCommand      = true;
            return await Task.FromResult(true);
        }

        command.ResultCommand = false;
        return await Task.FromResult(false);
    }

    #endregion

    #region IRequestHandler<UploadServerFileCommand,bool> Members

    /// <summary>
    ///     Handle upload file
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UploadServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //====================================Lấy ParentId nếu ParentId là empty====================================
        if (string.Compare(command.ParentId, Guid.Empty.ToString(), StringComparison.Ordinal) == 0)
        {
            var severFile = await _serverFileRepository.GetDataRoot();
            command.ParentId = severFile?.Id.ToString();
        }
        //==========================================================================================================

        var createUserId = _user.UserId;
        var folderUserId = "fol-" + Convert.ToDateTime(DateTime.Now).ToString("ddMMyyyy");
        // string.IsNullOrEmpty(command.ManagementId)
        //                    ? _user.StaffId
        //                    : command.ManagementId;
        var pathFolder  = "wwwroot\\Uploads\\FileFolder\\" + folderUserId + "\\";
        var pathFolder2 = "Uploads\\FileFolder\\"          + folderUserId + "\\";

        var nCore     = new NCoreHelper();
        var iPosition = await _serverFileRepository.GetMaxPosition(createUserId);
        if (command.IsLocal)
        {
            var lMultiFile = new List<ServerFileSendSave>();
            foreach (var iFile in command.FileModels)
            {
                if (iFile == null) continue;

                var extension = "."                + iFile.FileName.Split('.')[iFile.FileName.Split('.').Length - 1].ToLower();
                var fileName  = DateTime.Now.Ticks + extension;
                var iFolder   = nCore.CreateFolder(pathFolder).Result;
                var path      = Path.Combine(Directory.GetCurrentDirectory(), pathFolder + iFolder, fileName);

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await iFile.CopyToAsync(stream, cancellationToken);
                }

                var addSaveFile = new ServerFileSendSave
                {
                    Id            = Guid.NewGuid(),
                    Description   = "",
                    Folder        = iFolder,
                    Position      = iPosition++,
                    FileExtension = extension,
                    FileName      = string.IsNullOrEmpty(command.Name) ? iFile.FileName : command.Name,
                    FileNameRoot  = fileName,
                    FilePath      = pathFolder2 + iFolder + "\\" + fileName,
                    FileSize      = Convert.ToDouble(iFile.Length),
                    FileType      = command.FileType,
                    ManagementId = string.IsNullOrEmpty(command.ManagementId)
                        ? Guid.Empty
                        : Guid.Parse(command.ManagementId),
                    ParentId = command.ParentId,
                    StatusId = ActionStatusEnum.Active.Id,
                    UserId   = createUserId
                };

                if (command.FileType == FileTypeEnumeration.Image.Id)
                {
                    #region ===============================================Nén ảnh======================================

                    await nCore.Image_resize(path,
                                             new List<ImageResizeModel>
                                             {
                                                 // new()
                                                 // {
                                                 //     Output = Path.Combine(
                                                 //         Directory.GetCurrentDirectory(),
                                                 //         pathFolder + iFolder,
                                                 //         "64x64"    + extension),
                                                 //     Width  = 64,
                                                 //     Height = 64
                                                 // },
                                                 // new()
                                                 // {
                                                 //     Output = Path.Combine(
                                                 //         Directory.GetCurrentDirectory(),
                                                 //         pathFolder + iFolder,
                                                 //         "128x128"  + extension),
                                                 //     Width  = 128,
                                                 //     Height = 128
                                                 // },
                                                 new()
                                                 {
                                                     Output = Path.Combine(
                                                         Directory.GetCurrentDirectory(),
                                                         pathFolder + iFolder,
                                                         "500x320"  + extension),
                                                     Width  = 500,
                                                     Height = 500
                                                 }
                                             });

                    #endregion
                }

                if (command.FileModels.Count == 1)
                {
                    var iResult = await _serverFileQueries.AddFile(addSaveFile);
                    if (iResult > 0)
                    {
                        command.Id            = addSaveFile.Id;
                        command.FileName      = addSaveFile.FileName;
                        command.FilePath      = addSaveFile.FilePath;
                        command.ResultCommand = true;
                        return await Task.FromResult(true);
                    }

                    NotifyValidationErrors(NErrorHelper.upload_khong_thanh_cong);
                    return await Task.FromResult(false);
                }

                lMultiFile.Add(addSaveFile);
            }

            foreach (var items in lMultiFile) await _serverFileQueries.AddFile(items);

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        //======================================File từ đường dẫn bên ngoài=========================================
        var iCut       = command.Link.Split('.');
        var fileExtend = iCut.Length == 1 ? ".html" : iCut[^1].Length > 5 ? ".html" : "." + iCut[^1];

        var addFile = new ServerFile(new Guid(),
                                     fileExtend,
                                     command.Name,
                                     command.Name,
                                     command.Link,
                                     0,
                                     command.FileType,
                                     "fol-link",
                                     command.Description,
                                     command.ParentId,
                                     string.IsNullOrEmpty(command.ManagementId)
                                         ? Guid.Empty
                                         : Guid.Parse(command.ManagementId),
                                     command.Link,
                                     command.VideoType,
                                     iPosition,
                                     createUserId);
        await _serverFileRepository.AddAsync(addFile);

        if (Commit())
        {
            // CallDeQuy();
            //=================Ghi Log==================
            // await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
            //                                                   DateTime.Now,
            //                                                   SystemLogEnumeration.AddNew.Id,
            //                                                   _user.StaffId,
            //                                                   _user.StaffName,
            //                                                   GetType().Name,
            //                                                   "",
            //                                                   addFile.Id,
            //                                                   "",
            //                                                   JsonConvert.SerializeObject(command),
            //                                                   createUserId));
            //==========================================

            command.Id            = addFile.Id;
            command.FilePath      = command.Link;
            command.FileName      = addFile.FileName;
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.upload_khong_thanh_cong);
        return await Task.FromResult(false);
        //==========================================================================================================
    }

    #endregion

    #region IRequestHandler<UploadServerFile2023Command,bool> Members

    /// <summary>
    ///     Handle upload file
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UploadServerFile2023Command command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        //==========================================================================================================

        var pathFolder  = "wwwroot\\Uploads\\Img\\";
        var pathFolder2 = "Uploads\\Img\\";
        if (command.IsLocal)
        {
            // var lMultiFile = new List<ServerFileSendSave>();
            foreach (var iFile in command.FileModels)
            {
                if (iFile == null) continue;

                var fileNameTemp = DateTime.Now.Ticks;
                var extension    = "."          + iFile.FileName.Split('.')[iFile.FileName.Split('.').Length - 1].ToLower();
                var fileName     = fileNameTemp + extension;
                var path         = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, fileName);

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await iFile.CopyToAsync(stream, cancellationToken);
                }

                // var addSaveFile = new ServerFileSendSave
                // {
                //     Id            = Guid.NewGuid(),
                //     Description   = "",
                //     FileExtension = extension,
                //     FileName      = string.IsNullOrEmpty(command.Name) ? iFile.FileName : command.Name,
                //     FileNameRoot  = fileName,
                //     FilePath      = pathFolder2 + "\\" + fileName,
                //     FileSize      = Convert.ToDouble(iFile.Length),
                //     FileType      = command.FileType,
                //     ManagementId = string.IsNullOrEmpty(command.ManagementId)
                //         ? Guid.Empty
                //         : Guid.Parse(command.ManagementId),
                //     ParentId = command.ParentId,
                //     StatusId = ActionStatusEnum.Active.Id,
                //     UserId   = createUserId
                // };
                var nCore = new NCoreHelper();
                if (command.FileType == FileTypeEnumeration.Image.Id)
                {
                    #region ===============================================Nén ảnh======================================

                    await nCore.Image_resize(path,
                                             new List<ImageResizeModel>
                                             {
                                                 new()
                                                 {
                                                     Output = Path.Combine(
                                                         Directory.GetCurrentDirectory(),
                                                         pathFolder,
                                                         fileNameTemp + "_300x300" + extension),
                                                     Width  = 300,
                                                     Height = 300
                                                 }
                                             });

                    #endregion
                }

                if (command.FileModels.Count == 1)
                {
                    // var iResult = await _serverFileQueries.AddFile(addSaveFile);
                    // if (iResult > 0)
                    // {
                    //     command.Id            = addSaveFile.Id;
                    //     command.FileName      = addSaveFile.FileName;
                    //     command.FilePath      = addSaveFile.FilePath;
                    //     command.ResultCommand = true;
                    //     return await Task.FromResult(true);
                    // }
                    //
                    // NotifyValidationErrors(NErrorHelper.upload_khong_thanh_cong);
                    // command.FilePath      = pathFolder2 + "\\" + fileName;
                    command.Link          = pathFolder2 + "\\" + fileName;
                    command.ResultCommand = true;
                    return await Task.FromResult(true);
                }
                //
                // lMultiFile.Add(addSaveFile);
            }

            // foreach (var items in lMultiFile) await _serverFileQueries.AddFile(items);
            //
            // command.ResultCommand = true;
            // return await Task.FromResult(true);
        }

        //======================================File từ đường dẫn bên ngoài=========================================
        // var iCut       = command.Link.Split('.');
        // var fileExtend = iCut.Length == 1 ? ".html" : iCut[^1].Length > 5 ? ".html" : "." + iCut[^1];
        // command.FilePath      = command.Link;
        command.Link          = command.Link;
        command.ResultCommand = true;
        return await Task.FromResult(true);
        // var addFile = new ServerFile(new Guid(),
        //                              fileExtend,
        //                              command.Name,
        //                              command.Name,
        //                              command.Link,
        //                              0,
        //                              command.FileType,
        //                              "fol-link",
        //                              command.Description,
        //                              command.ParentId,
        //                              string.IsNullOrEmpty(command.ManagementId)
        //                                  ? Guid.Empty
        //                                  : Guid.Parse(command.ManagementId),
        //                              command.Link,
        //                              command.VideoType,
        //                              iPosition,
        //                              createUserId);
        // await _serverFileRepository.AddAsync(addFile);
        //
        // if (Commit())
        // {
        //     // CallDeQuy();
        //     //=================Ghi Log==================
        //     // await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
        //     //                                                   DateTime.Now,
        //     //                                                   SystemLogEnumeration.AddNew.Id,
        //     //                                                   _user.StaffId,
        //     //                                                   _user.StaffName,
        //     //                                                   GetType().Name,
        //     //                                                   "",
        //     //                                                   addFile.Id,
        //     //                                                   "",
        //     //                                                   JsonConvert.SerializeObject(command),
        //     //                                                   createUserId));
        //     //==========================================
        //
        //     command.Id            = addFile.Id;
        //     command.FilePath      = command.Link;
        //     command.FileName      = addFile.FileName;
        //     command.ResultCommand = true;
        //     return await Task.FromResult(true);
        // }
        //
        // NotifyValidationErrors(NErrorHelper.upload_khong_thanh_cong);
        // command.FilePath      = command.Link;
        // command.ResultCommand = true;
        // return await Task.FromResult(true);
        //==========================================================================================================
    }

    #endregion

    private void CallDeQuy()
    {
        var lModal = _serverFileRepository.GetListAsync().Result;
        _serverFileRepository.DeQuyLeftRight(lModal, 1, Guid.Empty.ToString());
        _serverFileRepository.SaveChanges();
    }

    #region ==============================FOLDER===================================

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddFolderServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var user    = _user;
        var iUserId = user.UserId;
        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;
        var iKey      = Guid.NewGuid();
        var iPosition = await _serverFileRepository.GetMaxPosition(iUserId);
        var rAdd = new ServerFile(iKey,
                                  command.FileName,
                                  command.Folder,
                                  command.Description,
                                  command.ParentId,
                                  iPosition,
                                  iUserId);
        await _serverFileRepository.AddAsync(rAdd);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                          DateTime.Now,
                                                          SystemLogEnumeration.AddNew.Id,
                                                          user.StaffId,
                                                          user.StaffName,
                                                          GetType().Name,
                                                          "Thêm mới thư mục: " + command.FileName,
                                                          iKey,
                                                          "",
                                                          JsonConvert.SerializeObject(command),
                                                          iUserId));
        //==========================================
        if (Commit())
        {
            CallDeQuy();
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateFolderServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;

        var existing = _serverFileRepository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(command.FileName,
                            existing.FileExtension,
                            existing.FilePath,
                            existing.FileSize,
                            existing.FileType,
                            command.FileName,
                            command.Description,
                            command.ParentId,
                            true,
                            existing.CloudId,
                            existing.CloudFolder,
                            Guid.Empty,
                            command.FileName,
                            existing.AvatarLink,
                            existing.Position,
                            _user.UserId);
            _serverFileRepository.Update(existing);
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              existing.Id,
                                                              JsonConvert.SerializeObject(existing),
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
        }

        if (Commit())
        {
            CallDeQuy();

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    /// <summary>
    ///     Handle cập nhật mã cha - con
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateParentServerFileCommand command, CancellationToken cancellationToken)
    {
        command.ResultCommand = false;
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        command.ParentId = string.IsNullOrEmpty(command.ParentId) ? Guid.Empty.ToString() : command.ParentId;

        var existing = _serverFileRepository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            existing.Update(existing.FileName,
                            existing.FileExtension,
                            existing.FilePath,
                            existing.FileSize,
                            existing.FileType,
                            existing.FileName,
                            existing.Description,
                            command.ParentId,
                            existing.IsFolder,
                            existing.CloudId,
                            existing.CloudFolder,
                            existing.ManagementId ?? Guid.Empty,
                            existing.FileName,
                            existing.AvatarLink,
                            existing.Position,
                            _user.UserId);
            _serverFileRepository.Update(existing);
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Update.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "Cập nhật mã cha con",
                                                              existing.Id,
                                                              JsonConvert.SerializeObject(existing),
                                                              JsonConvert.SerializeObject(command),
                                                              _user.UserId));
            //==========================================
        }

        if (Commit())
        {
            CallDeQuy();

            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
        return await Task.FromResult(false);
    }

    #endregion

    #region Fields

    private readonly IUser                 _user;
    private readonly IServerFileQueries    _serverFileQueries;
    private readonly IServerFileRepository _serverFileRepository;
    private readonly ISystemLogRepository  _systemLogRepository;

    #endregion
}