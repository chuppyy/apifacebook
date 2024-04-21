#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.ResponseDto;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.CompanyManagers.StaffManagers;

/// <summary>
///     Lớp interface query nhân viên
/// </summary>
public interface IStaffManagerQueries
{
#region =================================================STAFF=====================================================

    /// <summary>
    ///     [Phân trang] Trả về danh sách nhân viên
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StaffManagerPagingDto>> GetPaging(StaffManagerPagingViewModel model, int groupTable,
                                                       Guid                        managementId);

    /// <summary>
    ///     [Phân trang] Danh sách truy vết người dùng
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IEnumerable<UserTracingPagingDto>> GetPagingUserTracing(PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);

    /// <summary>
    ///     [Combobox] Trả về danh sách nhân viên
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [Combobox] Trả về danh sách tác giả
    /// </summary>
    /// <param name="vSearch">Giá trị tìm kiếm</param>
    /// <param name="pageSize">Số dòng hiển thị trên 1 trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxAuthorModal>> GetComboboxAuthor(string vSearch, int pageSize, int pageNumber);

    /// <summary>
    ///     Trả về danh sách nhân viên theo điều kiện của processingStreamId
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="processingStreamId"></param>
    /// <returns></returns>
    Task<IEnumerable<StaffManagerCheckSelectViewModel>> GetListCheckedProcessingStreamStaff(
        string vSearch, Guid processingStreamId);

    /// <summary>
    ///     [Combobox] Danh sách nhân viên by processingStreamId
    /// </summary>
    /// <param name="vSearch"></param>
    /// <param name="processingStreamId"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetListStaffByProcessingStreamId(string vSearch, Guid processingStreamId);

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

    Task<IEnumerable<UserModelDto>> GetUserCodeAsync();
    Task<IEnumerable<ComboboxIdNameDto>> GetComboboxWebAsync();
    Task<IEnumerable<WebsiteDto>> GetListInfoWebAsync(List<int> domainIds);
    Task<ConfigAnalyticsDto> GetConfigAnalyticsAsync();

    Task<IEnumerable<ReportUserGroupNewDto>> ReportUserGroupNewAsync(DateTime? endDate);
    Task<IEnumerable<TotalPostByGroupDto>> ReportPostAsync(IEnumerable<Guid> groupIds, DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<UserByOwnerDto>> GetListUserByOwnerAsync(string userId);
    Task<bool> UpdateRatioAsync(List<string> userIds, float ratio);

    #endregion
}