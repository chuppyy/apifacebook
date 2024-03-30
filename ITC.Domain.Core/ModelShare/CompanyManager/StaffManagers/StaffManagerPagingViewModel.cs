using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

/// <summary>
///     Modal phân trang STAFF-MANAGER
/// </summary>
public class StaffManagerPagingViewModel : PagingModel
{
    public Guid RoomManagerId     { get; set; }
    public Guid UserTypeManagerId { get; set; }
}