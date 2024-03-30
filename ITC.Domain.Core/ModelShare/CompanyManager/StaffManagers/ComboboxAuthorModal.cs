using System.Text.Json.Serialization;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

public class ComboboxAuthorModal : ComboboxModal
{
    /// <summary>
    ///     Mã tác giả
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Tổng số dữ liệu
    /// </summary>
    [JsonIgnore]
    public int TotalRecord { get; set; }
}