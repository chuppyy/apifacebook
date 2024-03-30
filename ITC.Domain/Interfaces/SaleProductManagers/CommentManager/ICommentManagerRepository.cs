using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.SaleProductManagers.CommentManager;

/// <summary>
///     Lớp interface repository comment
/// </summary>
public interface ICommentManagerRepository : IRepository<Models.SaleProductManagers.CommentManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<int> GetMaxPosition(Guid productId);

    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<Models.SaleProductManagers.CommentManager> model, int left, string parentId);
}