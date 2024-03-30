using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.SystemManagers;

namespace ITC.Domain.Interfaces.SystemManagers.ServerFiles;

/// <summary>
///     Class interface repository server file
/// </summary>
public interface IServerFileRepository : IRepository<ServerFile>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<ServerFile> model, int left, string parentId);

    /// <summary>
    ///     Trả về vị trí Max
    /// </summary>
    /// <param name="userId">Mã người tạo</param>
    /// <returns></returns>
    Task<int> GetMaxPosition(string userId);

    /// <summary>
    ///     Lấy dữ liệu ServerFile theo ParentId
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<ServerFile> GetByParentId(Guid value);

    /// <summary>
    ///     Lấy dữ liệu ServerFile là ROOT
    /// </summary>
    /// <returns></returns>
    Task<ServerFile> GetDataRoot();

    /// <summary>
    /// Danh sách server file
    /// </summary>
    /// <returns></returns>
    Task<List<ServerFile>> GetListAsync();
}