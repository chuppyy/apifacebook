using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using NCore.Modals;

namespace ITC.Domain.Interfaces.SystemManagers.ServerFiles;

/// <summary>
///     Class interface query ServerFile
/// </summary>
public interface IServerFileQueries
{
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
    Task<IEnumerable<ServerFilePagingDto>> GetPagingById(List<Guid> models);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [TreeView] Trả về danh sách các chức năng
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, string userId);

    /// <summary>
    ///     Danh sách file theo thư mục
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="pageSize">Số dòng hiển thị trên 1 trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <param name="pLeft">Chỉ số trái</param>
    /// <param name="pRight">Chỉ số phải</param>
    /// <param name="authorId">Tác giả</param>
    /// <param name="groupContentTypeId">Loại tài liệu</param>
    /// <returns></returns>
    Task<IEnumerable<ServerFileDetailPagingDto>> GetServerFileDetailPagingDto(
        string vSearch, int pageSize, int pageNumber, int pLeft, int pRight, string authorId, int groupContentTypeId);

    /// <summary>
    ///     Thêm dữ liệu server-file
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<int> AddFile(ServerFileSendSave model);

    /// <summary>
    ///     Trả về danh sách dữ liệu resize image
    /// </summary>
    /// <param name="model">Danh sách dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<ResizeImageDto>> ResizeImageType(ResizeImageModal model);
}