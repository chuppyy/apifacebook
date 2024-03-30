using System;
using System.Text.Json.Serialization;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Modal phân trang truy vết người dùng
/// </summary>
public class UserTracingPagingDto
{
    public              Guid     Id               { get; set; }
    public              string   Name             { get; set; }
    public              string   UserName         { get; set; }
    public              string   ConnectionId     { get; set; }
    public              DateTime TimeConnectEnd   { get; set; }
    public              DateTime TimeConnectStart { get; set; }
    public              bool     IsOnline         { get; set; }
    [JsonIgnore] public int      TotalRecord      { get; set; }
}