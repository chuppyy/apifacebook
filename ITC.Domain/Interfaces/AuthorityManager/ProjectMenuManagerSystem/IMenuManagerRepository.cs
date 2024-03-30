using System.Collections.Generic;
using ITC.Domain.Models.AuthorityManager;

namespace ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;

/// <summary>
///     Lớp interface repository danh sách chức năng
/// </summary>
public interface IMenuManagerRepository : IRepository<MenuManager>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<MenuManager> model, int left, string parentId);
}