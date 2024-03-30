#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsAttackManagers;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsAttackManagers;

/// <summary>
///     Class interface service NewsAttack
/// </summary>
public interface INewsAttackAppService
{
    /// <summary>
    ///     Danh sách file đính kèm theo NewsContentId
    /// </summary>
    /// <param name="newsContentId">mã bài viết</param>
    /// <returns></returns>
    Task<IEnumerable<NewsAttackPagingDto>> GetPaging(Guid newsContentId);
}