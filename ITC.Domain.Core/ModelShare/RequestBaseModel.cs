using System;
using System.ComponentModel.DataAnnotations;

namespace ITC.Domain.Core.ModelShare;

/// <summary>
///     Class truyền dữ liệu cố định
/// </summary>
public class RequestBaseModel
{
    /// <summary>
    ///     Id
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public string ManagementId { get; set; }

    /// <summary>
    ///     Mã người thao tác dữ liệu
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }
}