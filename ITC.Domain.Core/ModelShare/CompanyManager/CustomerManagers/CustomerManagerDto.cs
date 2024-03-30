using System;
using System.Text.Json.Serialization;

namespace ITC.Domain.Core.ModelShare.CompanyManager.CustomerManagers;

/// <summary>
///     Modal phân trang khách hàng
/// </summary>
public class CustomerManagerPagingDto
{
    public              Guid   Id          { get; set; }
    public              string Name        { get; set; }
    public              string Description { get; set; }
    public              string Phone       { get; set; }
    public              string Address     { get; set; }
    public              string Email       { get; set; }
    [JsonIgnore] public int    TotalRecord { get; set; }
}