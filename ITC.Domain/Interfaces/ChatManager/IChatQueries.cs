#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.ChatManager;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.ChatManager;

/// <summary>
///     Lớp interface query chat
/// </summary>
public interface IChatQueries
{
    /// <summary>
    ///     [Phân trang] Trả về danh sách Chat
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ChatEventModel>> GetPaging(Guid projectId);

    /// <summary>
    ///     [Phân trang] Trả về danh sách Chat
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ChatPagingManagerModel>> GetPagingManager(Guid projectId, PagingModel model);

    /// <summary>
    ///     Xóa nhiều dữ liệu
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(List<Guid> model);
}