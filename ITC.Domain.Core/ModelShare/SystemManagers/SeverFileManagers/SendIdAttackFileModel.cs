using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     Trả về danh sách ID của file đính kèm
/// </summary>
public class SendIdAttackFileModel
{
    /// <summary>
    ///     Kết quả trả về
    /// </summary>
    public bool ResultCommand { get; set; }

    /// <summary>
    ///     Danh sách ID
    /// </summary>
    public List<Guid> Models { get; set; }
}