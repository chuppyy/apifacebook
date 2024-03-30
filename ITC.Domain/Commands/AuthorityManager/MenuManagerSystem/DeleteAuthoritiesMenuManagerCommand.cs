#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command xóa Chat
/// </summary>
public class DeleteAuthoritiesMenuManagerCommand : AuthoritiesMenuManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Danh sách các ID chủ đề cần xóa</param>
    public DeleteAuthoritiesMenuManagerCommand(List<Guid> model)
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