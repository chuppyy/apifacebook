using System;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     Cập nhật giá trị quyền sử dụng cho chức năng được cấp
/// </summary>
public class SortMenuPermissionByAuthoritiesModel
{
    /// <summary>
    ///     Mã quyền chi tiết
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Giá trị quyên
    /// </summary>
    public int Value { get; set; }
}