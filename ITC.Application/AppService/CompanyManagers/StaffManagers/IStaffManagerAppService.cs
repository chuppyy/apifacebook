#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Models.CompanyManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.CompanyManagers.StaffManagers;

/// <summary>
///     Class interface service nhân viên
/// </summary>
public interface IStaffManagerAppService
{
#region ==================================STAFF==============================

    /// <summary>
    ///     Thêm mới nhân viên
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Add(StaffManagerEventModel model);

    /// <summary>
    ///     Xóa nhân viên
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    StaffManagerEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật nhân viên
    /// </summary>
    /// <param name="model"></param>
    Task<bool> Update(StaffManagerEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách nhân viên
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [Combobox] Danh sách tác giả
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="pageSize">Số dòng hiển thị trên 1 trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxAuthorModal>> GetComboboxAuthor(string vSearch, int pageSize, int pageNumber);

    /// <summary>
    ///     Danh sách nhân viên có gán biến checked-select
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="processingStreamId"></param>
    /// <returns></returns>
    Task<IEnumerable<StaffManagerCheckSelectViewModel>> GetListChecked(string vSearch, Guid processingStreamId);

    /// <summary>
    ///     [Phân trang] Danh sách nhân viên
    /// </summary>
    /// <param name="model"></param>
    /// <param name="groupTable"></param>
    /// <returns></returns>
    Task<IEnumerable<StaffManagerPagingDto>> GetPaging(StaffManagerPagingViewModel model, int groupTable);

    /// <summary>
    ///     [Phân trang] Danh sách truy vết người dùng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<UserTracingPagingDto>> GetPagingUserTracing(PagingModel model);

    /// <summary>
    ///     Quản lý ảnh đại diện
    /// </summary>
    /// <param name="model"></param>
    Task<bool> AvatarManager(UploadImageStaffEventModel model);

    /// <summary>
    ///     Trả về ảnh đại diện của tài khoản
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    string AvatarLink(string userid);

    /// <summary>
    ///     Trả về đường dẫn trang chủ của tài khoản
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    string GetUrlHomePage(string userid);

    /// <summary>
    ///     [Combobox] Danh sách nhân viên by processingStreamId
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="processingStreamId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetListStaffByProcessingStreamId(string vSearch, Guid processingStreamId);

    /// <summary>
    ///     Lấy theo UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<StaffManager> GetByUserId(string userId);

    /// <summary>
    ///     Danh sách người dùng trong hệ thống
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetStaffInSystem();

    /// <summary>
    ///     Trả về danh sách StaffManagement theo mã người dùng
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<StaffManagerByUserDto> GetByUserId2(string userId);

#endregion
}