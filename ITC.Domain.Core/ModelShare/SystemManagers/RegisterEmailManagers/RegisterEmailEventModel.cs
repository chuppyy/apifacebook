using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.RegisterEmailManagers;

/// <summary>
///     [Model] Đăng ký email
/// </summary>
public class RegisterEmailEventModel : PublishModal
{
    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }
}