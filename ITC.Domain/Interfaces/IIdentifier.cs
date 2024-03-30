namespace ITC.Domain.Interfaces;

/// <summary>
///     Đại diện cho một đối tượng được xác định bởi tên
/// </summary>
public interface IIdentifier
{
#region Properties

    /// <summary>
    ///     Tên đối tượng
    /// </summary>
    string Name { get; }

#endregion
}