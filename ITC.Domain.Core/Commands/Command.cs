#region

using System;
using FluentValidation.Results;
using ITC.Domain.Core.Events;

#endregion

namespace ITC.Domain.Core.Commands;

public abstract class Command : Message
{
#region Constructors

    protected Command()
    {
        Timestamp = DateTime.Now;
    }

#endregion

#region Methods

    public abstract bool IsValid();

#endregion

#region Properties

    /// <summary>
    ///     Người tạo
    /// </summary>
    public string CreateBy { get; set; }

    /// <summary>
    ///     Người chỉnh sửa
    /// </summary>
    public string ModifiedBy { get; set; }

    public int      PortalId  { get; set; }
    public DateTime Timestamp { get; }

    /// <summary>
    ///     Kết quả xử lý trả về của command
    /// </summary>
    public bool ResultCommand { get; set; }

    public ValidationResult ValidationResult { get; set; }

#endregion
}