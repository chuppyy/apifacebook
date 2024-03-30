using ITC.Domain.Core.ModelShare.AuthorityManager;

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command thêm phân quyền
/// </summary>
public class AddAuthoritiesMenuManagerCommand : AuthoritiesMenuManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddAuthoritiesMenuManagerCommand(AuthoritiesMenuManagerEventModel model)
    {
        Models    = model.Models;
        ProjectId = model.ProjectId;
        Name      = model.Name;
    }

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