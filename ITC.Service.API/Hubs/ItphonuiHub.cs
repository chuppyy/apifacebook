using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using Microsoft.AspNetCore.SignalR;

namespace ITC.Service.API.Hubs;

/// <summary>
///     Kết nối Hub
/// </summary>
public class ItphonuiHub : Hub
{
    private readonly IStaffManagerRepository _staffManagerRepository;

#region Constructors

    /// <summary>
    /// </summary>
    /// <param name="staffManagerRepository"></param>
    public ItphonuiHub(IStaffManagerRepository staffManagerRepository)
    {
        _staffManagerRepository = staffManagerRepository;
    }

#endregion

#region Fields

    private string ConnectionId => Context.ConnectionId;

#endregion

#region Methods

    /// <inheritdoc />
    public override Task OnConnectedAsync()
    {
        var token = DecryptionToken();
        Guid.TryParse(token.Payload["StaffId"].ToString(), out var staffGuid);
        var staffInfo = _staffManagerRepository.GetAsync(staffGuid).Result;
        if (staffInfo != null)
        {
            staffInfo.TimeConnectStart = DateTime.Now;
            staffInfo.TimeConnectEnd   = null;
            staffInfo.IsOnline         = true;
            staffInfo.ConnectionId     = ConnectionId;

            _staffManagerRepository.SaveChanges();
            // Clients.All.SendAsync("UserConnectSystem");
        }

        return base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var token = DecryptionToken();
        Guid.TryParse(token.Payload["StaffId"].ToString(), out var staffGuid);
        var staffInfo = _staffManagerRepository.GetAsync(staffGuid).Result;
        if (staffInfo != null)
        {
            staffInfo.TimeConnectStart = staffInfo.TimeConnectStart;
            staffInfo.TimeConnectEnd   = DateTime.Now;
            staffInfo.IsOnline         = false;
            staffInfo.ConnectionId     = "";

            _staffManagerRepository.SaveChanges();
        }

        return base.OnDisconnectedAsync(exception);
    }

    private JwtSecurityToken DecryptionToken()
    {
        var accessToken = Context.GetHttpContext()!.Request.Query["access_token"];
        var handler     = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(accessToken);
    }

    /// <summary>
    ///     Đá người dùng khỏi hệ thống
    /// </summary>
    public async Task SendUserLogOut(string connectionId)
    {
        await Clients.Client(connectionId).SendAsync("UserLogout");
    }

    /// <summary>
    ///     Gửi tin nhắn
    /// </summary>
    public async Task SendMessage(string user, string message)
    {
        // var token = DecryptionToken();
        // Guid.TryParse(token.Payload["StaffProjectId"].ToString(), out var staffProjectId);
        // var rAdd = new Chat(Guid.NewGuid(),
        //                     Guid.Parse(user),
        //                     Guid.Empty,
        //                     message,
        //                     staffProjectId,
        //                     0,
        //                     user);
        // await _chatRepository.AddAsync(rAdd);
        // var iResult = _chatRepository.SaveChanges();
        // if (iResult > 0)
        //     await Clients.All.SendAsync("ReceiveMessage", new SendMessageModel()
        //     {
        //         UserId  = user,
        //         Content = message
        //     });
        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage");
    }

#endregion
}

// /// <summary>
// /// Gửi tin nhắn về FE
// /// </summary>
// public class SendMessageModel
// {
//     /// <summary>
//     /// Mã người dùng
//     /// </summary>
//     public string UserId { get; set; }
//
//     /// <summary>
//     /// Nội dung tin nhắn
//     /// </summary>
//     public string Content { get; set; }
// }