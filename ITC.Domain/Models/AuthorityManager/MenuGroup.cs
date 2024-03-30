using System;

namespace ITC.Domain.Models.AuthorityManager;

public class MenuGroup
{
#region Properties

    /// <summary>
    ///     Mã nhóm
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Tên nhóm
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Số thứ tự
    /// </summary>
    public int NumberOf { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Positon { get; set; }

    /// <summary>
    ///     ICon
    /// </summary>
    public int ManagerIconId { get; set; }

    /// <summary>
    ///     Đường dẫn mặc định
    /// </summary>
    public string LinkDefault { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Phiên bản sử dụng
    /// </summary>
    public int Version { get; set; }

#endregion
}