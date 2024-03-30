#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.SystemManagers.RegisterEmailManagers;

/// <summary>
///     Command RegisterEmailCommand
/// </summary>
public abstract class RegisterEmailCommand : Command
{
    /// <summary>
    ///     Mã Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Địa chỉ email
    /// </summary>
    public string Email { get; set; }
}