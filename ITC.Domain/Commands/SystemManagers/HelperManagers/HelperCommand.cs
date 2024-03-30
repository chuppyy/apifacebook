#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.SystemManagers.HelperManagers;

/// <summary>
///     Command HelperCommand
/// </summary>
public abstract class HelperCommand : Command
{
    /// <summary>
    ///     Mã Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Mã dữ liệu table
    /// </summary>
    public int TableId { get; set; }

    /// <summary>
    ///     Dữ liệu trạng thái
    /// </summary>
    public int FlagKey { get; set; }
}