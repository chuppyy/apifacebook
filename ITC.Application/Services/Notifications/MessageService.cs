#region

using System;
using System.Net;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.Notification;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

#endregion

namespace ITC.Application.Services.Notifications;

public class MessageService : IEmailSender
{
#region Fields

    private readonly EmailSetting _email;

#endregion

#region Constructors

    public MessageService(
        IOptions<EmailSetting> emailOption
    )
    {
        _email = emailOption.Value;
    }

#endregion

#region IEmailSender Members

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_email.FromName, _email.FromAddress));
        emailMessage.To.Add(new MailboxAddress(string.Empty,      email));
        emailMessage.Subject = subject;
        emailMessage.Body    = new TextPart(TextFormat.Html) { Text = message };
        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_email.MailServerAddress, _email.MailServerPort);
                await client.AuthenticateAsync(new NetworkCredential(_email.UserName, _email.UserPassword));
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception)
        {
            //_sysLogger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(e));
        }
    }

#endregion
}

public class EmailSetting
{
#region Properties

    public string FromAddress       { get; set; }
    public string FromName          { get; set; }
    public string MailServerAddress { get; set; }
    public int    MailServerPort    { get; set; }
    public string UserName          { get; set; }
    public string UserPassword      { get; set; }

#endregion
}