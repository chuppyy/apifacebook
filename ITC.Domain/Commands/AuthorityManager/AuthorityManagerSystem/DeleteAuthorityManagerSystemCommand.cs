#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Command xóa quyền sử dụng
/// </summary>
public class DeleteAuthorityManagerSystemCommand : AuthorityManagerSystemCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model"></param>
    public DeleteAuthorityManagerSystemCommand(List<Guid> model)
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