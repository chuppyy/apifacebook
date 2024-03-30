#region

#endregion

using System.Threading.Tasks;
using ITC.Domain.Models.Itphonui;

namespace ITC.Domain.Interfaces.Itphonui.ProjectManagers;

/// <summary>
///     Lớp interface repository quản lý dự án
/// </summary>
public interface IProjectManagerRepository : IRepository<ProjectManager>
{
    /// <summary>
    ///     Lấy dữ liệu theo hostName
    /// </summary>
    /// <param name="hostName"></param>
    /// <returns></returns>
    Task<ProjectManager> GetByHostName(string hostName);

    /// <summary>
    ///     Lấy dữ liệu mới nhất
    /// </summary>
    /// <returns></returns>
    Task<string> GetMaxCode();

    /// <summary>
    ///     Lấy dữ liệu theo hostName
    /// </summary>
    /// <param name="hostName"></param>
    /// <returns></returns>
    Task<ProjectManager> GetByOriginal(string hostName);
}