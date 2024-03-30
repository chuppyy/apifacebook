using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SportManagers;

/// <summary>
///     Nộp hồ sơ
/// </summary>
public class SportSubmitDocument : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportSubmitDocument(Guid   id, Guid managementId, Guid projectId, Guid fileId, int typeId, int position,
                               string createdBy = null)
        : base(id, createdBy)
    {
        StatusId     = ActionStatusEnum.Active.Id;
        FileId       = fileId;
        TypeId       = typeId;
        Position     = position;
        ManagementId = managementId;
        ProjectId    = projectId;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SportSubmitDocument()
    {
    }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid ManagementId { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    ///     Loại file
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }
}