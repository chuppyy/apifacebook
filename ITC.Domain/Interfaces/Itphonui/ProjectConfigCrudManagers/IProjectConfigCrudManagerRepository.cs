#region

#endregion

using System;
using System.Threading.Tasks;
using ITC.Domain.Models.Itphonui;

namespace ITC.Domain.Interfaces.Itphonui.ProjectConfigCrudManagers;

/// <summary>
///     Lớp interface repository cấu hình thơời gian
/// </summary>
public interface IProjectConfigCrudManagerRepository : IRepository<ProjectConfigCrudManager>
{
    /// <summary>
    ///     Trả về ProjectConfigCrud theo ProjectId
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<ProjectConfigCrudManager> GetByProjectId(Guid projectId);
}