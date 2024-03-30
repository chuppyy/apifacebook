using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Modal phân trang STAFF-MANAGER
/// </summary>
public class StaffManagerCheckSelectViewModel : ComboboxModal
{
    public string Checked { get; set; }
}