#region

using System.Collections.Generic;
using ITC.Domain.Core.Events;

#endregion

namespace ITC.Infra.Data.EventSourcing;

/// <summary>
///     Lớp lưu trữ các dữ liệu đăng ký
/// </summary>
public class HistoryContainer
{
#region Static Fields and Constants

    private static HistoryContainer _container;

#endregion

#region Fields

    /// <summary>
    ///     Từ điển chứa các đối tượng
    /// </summary>
    private readonly Dictionary<string, RegisterValue> _containerDict = new();

#endregion

#region Properties

    /// <summary>
    ///     Kho chứa các sự kiện
    /// </summary>
    public static HistoryContainer Container => _container ?? (_container = new HistoryContainer());

#endregion

#region Methods

    /// <summary>
    ///     Lấy dữ liệu từ
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public RegisterValue GetValue(string @event)
    {
        if (_containerDict.ContainsKey(@event))
            return _containerDict[@event];
        return null;
    }

    /// <summary>
    ///     Đăng ký dữ liệu vào kho chứa
    /// </summary>
    /// <param name="key">Khóa đăng ký</param>
    /// <param name="displayName">Tên hiển thị</param>
    /// <param name="message">Tin nhắn diễn tả nghĩa</param>
    public void Register<T>(string displayName, string message) where T : StoredEvent
    {
        var _key = typeof(T).Name;
        if (!_containerDict.ContainsKey(_key))
            _containerDict.Add(_key, new RegisterValue(displayName, message));
    }

#endregion
}