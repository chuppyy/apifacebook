#region

using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.IconManagerSystem;

/// <summary>
///     Command danh sách icon
/// </summary>
public abstract class ManagerIconCommand : Command
{
    /// <summary>
    ///     Mã nhóm
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Tên nhóm
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Xem trước
    /// </summary>
    public string View { get; set; }
}