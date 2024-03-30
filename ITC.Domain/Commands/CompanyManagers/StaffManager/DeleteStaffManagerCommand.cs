#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.CompanyManagers.StaffManager;

/// <summary>
///     Command xóa nhân viên
/// </summary>
public class DeleteStaffManagerCommand : StaffManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="ids">Danh sách id cần xóa</param>
    public DeleteStaffManagerCommand(List<Guid> model)
    {
        Model = model;
    }

#endregion

#region Properties

    /// <summary>
    ///     Id cần xóa
    /// </summary>
    public List<Guid> Model { get; set; }

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