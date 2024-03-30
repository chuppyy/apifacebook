#region

using System.Threading.Tasks;

#endregion

namespace ITC.Domain.Interfaces.Notification;

public interface IEmailSender
{
#region Methods

    /// <summary>
    ///     Gửi email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendEmailAsync(string email, string subject, string message);

#endregion
}