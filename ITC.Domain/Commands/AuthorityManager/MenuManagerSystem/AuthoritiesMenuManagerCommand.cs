#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Commands;
using ITC.Domain.Core.ModelShare.AuthorityManager;

#endregion

namespace ITC.Domain.Commands.AuthorityManager.MenuManagerSystem;

/// <summary>
///     Command quyền sử dụng
/// </summary>
public abstract class AuthoritiesMenuManagerCommand : Command
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid CompanyId { get; set; }

    public List<MenuByAuthoritiesSaveModel> Models { get; set; }
    public string                           Name   { get; set; }
}