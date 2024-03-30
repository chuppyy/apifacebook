#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using NCore.Modals;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsDomainManagers;

/// <summary>
///     Class interface service loại nhóm tin
/// </summary>
public interface INewsDomainAppService
{
#region Methods

    /// <summary>
    ///     Thêm mới loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Add(NewsDomainEventModel model);

    /// <summary>
    ///     Xóa loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Delete(DeleteModal model);

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    NewsDomainEventModel GetById(Guid id);

    /// <summary>
    ///     Cập nhật loại nhóm tin
    /// </summary>
    /// <param name="model"></param>
    bool Update(NewsDomainEventModel model);

    /// <summary>
    ///     [Combobox] Danh sách loại nhóm tin
    /// </summary>
    /// <param name="vSearch"></param>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch);

    /// <summary>
    ///     [Phân trang] Danh sách loại nhóm tin
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    /// <returns></returns>
    Task<IEnumerable<NewsDomainPagingDto>> GetPaging(PagingModel model);

    /// <summary>
    ///     Chạy lập lịch
    /// </summary>
    Task SchedulerStart();

    /// <summary>
    /// Lấy cấu hình lập lịch
    /// </summary>
    /// <param name="authorities"></param>
    /// <returns></returns>
    Task<int> GetScheduleConfig(string authorities);

    /// <summary>
    /// Lưu cấu hình lập lịch
    /// </summary>
    /// <returns></returns>
    Task<int> GetScheduleSave(int id);

    #endregion
}