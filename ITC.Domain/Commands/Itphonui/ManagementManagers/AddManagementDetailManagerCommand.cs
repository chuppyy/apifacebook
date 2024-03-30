using System;
using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;

namespace ITC.Domain.Commands.Itphonui.ManagementManagers;

/// <summary>
///     Command thêm quản lý đơn vị
/// </summary>
public class AddManagementDetailManagerCommand : ManagementManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public AddManagementDetailManagerCommand(ManagementDetailManagerEventModel model)
    {
        Models    = model.Models;
        ProjectId = model.ProjectId;
    }

#endregion

    public List<Guid> Models    { get; set; }
    public Guid       ProjectId { get; set; }

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}