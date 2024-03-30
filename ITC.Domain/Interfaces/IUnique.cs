namespace ITC.Domain.Interfaces;

/// <summary>
///     Đại diện cho một đối tượng được xác định bởi mã
/// </summary>
public interface IUnique
{
#region Properties

    /// <summary>
    ///     Mã đối tượng
    /// </summary>
    string Code { get; set; }

#endregion
}