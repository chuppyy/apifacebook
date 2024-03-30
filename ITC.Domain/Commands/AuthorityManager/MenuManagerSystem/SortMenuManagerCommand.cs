using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command sắp xếp chức năng
/// </summary>
public class SortMenuManagerCommand : MenuManagerCommand
{
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

#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model"></param>
    public SortMenuManagerCommand(SortMenuManagerEventModel model)
    {
        Models = model.Models;
    }

    public List<SortMenuManagerDto> Models { get; set; }

#endregion
}