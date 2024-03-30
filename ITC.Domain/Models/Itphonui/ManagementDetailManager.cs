using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.Itphonui;

/// <summary>
///     Quản lý đơn vị - chi tiết theo từng dự án
/// </summary>
public class ManagementDetailManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ManagementDetailManager(Guid id, Guid projectId, Guid managementManagerId, string createdBy = null)
        : base(id, createdBy)
    {
        ManagementManagerId = managementManagerId;
        ProjectId           = projectId;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ManagementDetailManager()
    {
    }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid ManagementManagerId { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }
}