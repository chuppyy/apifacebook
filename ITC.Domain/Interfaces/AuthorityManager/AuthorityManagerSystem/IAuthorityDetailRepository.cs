using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.AuthorityManager;

namespace ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Lớp interface repository phân quyền chi tiết
/// </summary>
public interface IAuthorityDetailRepository : IRepository<AuthorityDetail>
{
    /// <summary>
    ///     Trả về vị trí lịch sử
    /// </summary>
    /// <param name="authoritiesId">Mã quyền sử dụng</param>
    /// <returns></returns>
    Task<int> GetMaxHistoryPosition(Guid authoritiesId);

    /// <summary>
    ///     Danh sách ID câu hỏi con theo mã câu hỏi cha
    /// </summary>
    /// <param name="authorityId">mã quyền sử dụng</param>
    /// <returns></returns>
    Task<List<AuthorityDetail>> GetListAuthorityDetailChild(Guid authorityId);

    /// <summary>
    ///     Danh sách ID câu hỏi con theo mã câu hỏi cha
    /// </summary>
    /// <param name="authorityId">mã quyền sử dụng</param>
    /// <param name="menuManagerId">mã menu</param>
    /// <returns></returns>
    Task<AuthorityDetail> GetByMenuManager(Guid authorityId, Guid menuManagerId);
}