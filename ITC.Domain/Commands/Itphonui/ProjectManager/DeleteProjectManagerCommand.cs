#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.Itphonui.ProjectManager;

/// <summary>
///     Command xóa quản lý dự án
/// </summary>
public class DeleteProjectManagerCommand : ProjectManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Danh sách id cần xóa</param>
    public DeleteProjectManagerCommand(List<Guid> model)
    {
        ListModel = model;
    }

#endregion

#region Properties

    /// <summary>
    ///     Id cần xóa
    /// </summary>
    public List<Guid> ListModel { get; set; }

#endregion

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