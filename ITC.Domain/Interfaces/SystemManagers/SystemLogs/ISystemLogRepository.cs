using ITC.Domain.Core.Events;

namespace ITC.Domain.Interfaces.SystemManagers.SystemLogs;

/// <summary>
///     Class interface repository nhật ký hệ thống
/// </summary>
public interface ISystemLogRepository : IRepository<SystemLog>
{
    /// <summary>
    ///     Lưu log
    /// </summary>
    /// <param name="model"></param>
    void Store(SystemLog model);
}