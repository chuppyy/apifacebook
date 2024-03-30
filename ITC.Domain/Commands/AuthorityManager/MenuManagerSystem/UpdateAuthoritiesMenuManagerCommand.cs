using System;
using ITC.Domain.Core.ModelShare.AuthorityManager;

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command cập nhật phân quyền
/// </summary>
public class UpdateAuthoritiesMenuManagerCommand : AuthoritiesMenuManagerCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateAuthoritiesMenuManagerCommand(AuthoritiesMenuManagerEventModel model)
    {
        Models    = model.Models;
        ProjectId = model.ProjectId;
        Name      = model.Name;
        CompanyId = model.CompanyId;
        Id        = Guid.Parse(model.AuthotitiesId);
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