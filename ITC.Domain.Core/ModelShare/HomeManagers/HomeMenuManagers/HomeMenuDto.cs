using System;

namespace ITC.Domain.Core.ModelShare.HomeManagers.HomeMenuManagers;

/// <summary>
///     [Model] Liên kết với nhóm tin
/// </summary>
public class HomeMenuNewsGroupModel
{
    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên nội dung
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã nhóm cha
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Tên nhóm cha
    /// </summary>
    public string ParentName { get; set; }
}