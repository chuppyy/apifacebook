#region

using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.IconManagerSystem;

/// <summary>
///     Command xóa icon
/// </summary>
public class DeleteManagerIconCommand : ManagerIconCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Danh sách id cần xóa</param>
    public DeleteManagerIconCommand(List<int> model)
    {
        Model = model;
    }

#endregion

#region Properties

    /// <summary>
    ///     Id cần xóa
    /// </summary>
    public List<int> Model { get; set; }

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