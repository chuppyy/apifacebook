#region

using System.Collections.Generic;
using System.Linq;
using ITC.Domain.Interfaces.AuthorityManager.ProjectMenuManagerSystem;
using ITC.Domain.Models.AuthorityManager;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.AuthorityManager.ProjectMenuManagerSystem;

/// <summary>
///     Lớp repository danh sách chức năng
/// </summary>
public class MenuManagerRepository : Repository<MenuManager>, IMenuManagerRepository
{
#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="context"></param>
    public MenuManagerRepository(EQMContext context) : base(context)
    {
    }

#endregion

    /// <inheritdoc cref="DeQuyLeftRight" />
    public int DeQuyLeftRight(List<MenuManager> model, int left, string parentId)
    {
        var dsItem = model.Where(x => x.ParentId == parentId);
        foreach (var item in dsItem)
        {
            item.MLeft  = left;
            left        = DeQuyLeftRight(model, left + 1, item.Id.ToString());
            item.MRight = left;
            left++;
        }

        return left;
    }
}