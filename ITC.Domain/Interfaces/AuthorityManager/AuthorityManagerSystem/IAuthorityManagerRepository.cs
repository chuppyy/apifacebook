using System;
using System.Threading.Tasks;
using ITC.Domain.Models.AuthorityManager;

namespace ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;

/// <summary>
///     Lớp interface repository phân quyền
/// </summary>
public interface IAuthorityManagerRepository : IRepository<Authority>
{
    /// <summary>
    ///     LoadAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Authority> LoadAsync(Guid id);
}