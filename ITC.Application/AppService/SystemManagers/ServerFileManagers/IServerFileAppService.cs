#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.SystemManagers.ServerFileManagers;

/// <summary>
///     Class interface service ServerFile
/// </summary>
public interface IServerFileAppService
{
    /// <summary>
    ///     Upload File
    /// </summary>
    /// <param name="model">model truyền từ FE</param>
    /// <returns></returns>
    Task<Guid> UploadFile(UploadFileEventModel model);
    
    /// <summary>
    ///     Upload File
    /// </summary>
    /// <param name="model">model truyền từ FE</param>
    /// <returns></returns>
    Task<string> UploadServerFile(UploadFileEventModel model);

    /// <summary>
    ///     Upload File Attack
    /// </summary>
    /// <param name="model">model truyền từ FE</param>
    /// <returns></returns>
    Task<SendIdAttackFileModel> UploadFileAttack(UploadFileEventModel model);

    /// <summary>
    ///     Cập nhật fileName
    /// </summary>
    /// <param name="model">model truyền từ FE</param>
    /// <returns></returns>
    bool UpdateFileName(UpdateFileNameModal model);

    /// <summary>
    ///     Danh sách file
    /// </summary>
    /// <param name="search">Giá trị tìm kiếm</param>
    /// <param name="workManagerId">Mã công việc</param>
    /// <returns></returns>
    Task<IEnumerable<ServerFilePagingDto>> GetPaging(string search, Guid workManagerId);

    /// <summary>
    ///     Danh sách file theo ID
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ServerFilePagingDto>> GetPagingById(string attackId);

    /// <summary>
    ///     Xóa file
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Call đệ quy
    /// </summary>
    Task<bool> CallDeQuy();

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(TreeViewPagingModelLibrary model);

#region ==============================================FOLDER========================================================

    /// <summary>
    ///     Thêm mới thư mục
    /// </summary>
    /// <param name="model"></param>
    bool FolderAdd(FolderServerFileEvent model);

    /// <summary>
    ///     Cập nhật thư mục
    /// </summary>
    /// <param name="model"></param>
    bool FolderUpdate(FolderServerFileEvent model);

    /// <summary>
    ///     Cập nhật mã cha - con
    /// </summary>
    /// <param name="model"></param>
    bool ParentUpdate(FolderServerFileEvent model);

    /// <summary>
    ///     Lấy dữ liệu thư mục theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    FolderServerFileEvent GetByFolderId(Guid id);

#endregion

#region ==============================================DETAIL========================================================

    /// <summary>
    ///     Danh sách file
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="authorId"></param>
    /// <param name="serverFileId">Mã file</param>
    /// <param name="groupContentTypeId">Loại file</param>
    /// <returns></returns>
    Task<IEnumerable<ServerFileDetailPagingDto>> GetServerFileDetailPagingDto(
        string vSearch, int pageSize, int pageNumber, string authorId, Guid serverFileId, int groupContentTypeId);

    /// <summary>
    ///     Thêm mới dữ liệu từ đường dẫn khác
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UploadDifference(UploadDifferenceEventModal model);

    /// <summary>
    ///     Xem chi tiết file
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ServerFileDetailInfo> ViewFile(Guid id);

    /// <summary>
    ///     Cập nhật loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateNameFile(UpdateFileNameModal model);

    /// <summary>
    ///     Trả về danh sách dữ liệu resize image
    /// </summary>
    /// <param name="model">Danh sách dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ResizeImageDto>> ResizeImageType(ResizeImageModal model);

#endregion
}