#region

using ITC.Domain.StoredEvents.Account;
using ITC.Infra.Data.EventSourcing;
using ITC.Service.API.Controllers;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
///     Lịch sử hệ thống
/// </summary>
public static class HistoryHelper
{
#region Methods

    /// <summary>
    ///     Đăng ký lịch sử hệ thống
    /// </summary>
    public static void RegisterHistory()
    {
    #region Hệ thống

        // Account 
        HistoryContainer.Container.Register<AccountActivedEvent>("Kích hoạt tài khoản", "đã kích hoạt tài khoản");
        HistoryContainer.Container.Register<ReissusePasswordEvent>("Cấp lại mật khẩu",
                                                                   "đã cấp lại mật khẩu cho tài khoản");
        HistoryContainer.Container.Register<AccountAddEmailEvent>("Thêm email", "đã thêm email");
        //Đăng nhập
        HistoryContainer.Container.Register<LoginStoredEvent>("Đăng nhập", "đã đăng nhập vào hệ thống từ địa chỉ");

    #endregion
    }

#endregion
}