using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SortMenuManagers;

/// <summary>
///     [Model] nhận dữ liệu từ FE
/// </summary>
public class SortMenuManagerEventModel : PublishModal
{
    public List<SortMenuManagerDto> Models { get; set; }
}