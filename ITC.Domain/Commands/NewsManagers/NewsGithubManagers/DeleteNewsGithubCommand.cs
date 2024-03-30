#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsGithubManagers;

/// <summary>
///     Command xóa loại nhóm tin
/// </summary>
public class DeleteNewsGithubCommand : NewsGithubCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Danh sách id cần xóa</param>
    public DeleteNewsGithubCommand(List<Guid> model)
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