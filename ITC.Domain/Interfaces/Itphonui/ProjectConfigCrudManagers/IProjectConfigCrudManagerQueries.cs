#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace ITC.Domain.Interfaces.Itphonui.ProjectConfigCrudManagers;

/// <summary>
///     Lớp interface query cấu hình thời gian
/// </summary>
public interface IProjectConfigCrudManagerQueries
{
    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}