using System;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     Trả về quyền truy cập theo mã chức năng được cấp
/// </summary>
public class PermissionByAuthoritiesModel
{
    /// <summary>
    ///     Mã chức năng chi tiết
    /// </summary>
    public Guid AuthoritiesDetailId { get; set; }

    /// <summary>
    ///     Giá trị quyền
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    ///     Tên quyền
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Đã cấp quyền
    /// </summary>
    public int Checked { get; set; }
}