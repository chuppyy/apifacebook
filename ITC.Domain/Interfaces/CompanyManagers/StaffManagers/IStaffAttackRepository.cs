#region

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.CompanyManagers;

namespace ITC.Domain.Interfaces.CompanyManagers.StaffManagers;

/// <summary>
///     Lớp interface repository người dùng - file đính kèm
/// </summary>
public interface IStaffAttackRepository : IRepository<StaffAttackManager>
{
    /// <summary>
    ///     Danh sách ID theo mã người dùng
    /// </summary>
    /// <param name="staffId">mã người dùng</param>
    /// <returns></returns>
    Task<List<Guid>> GetListDataByStaffId(Guid staffId);

    /// <summary>
    ///     Trả về StaffAttackManager theo môn đng ký thi đấu
    /// </summary>
    /// <param name="staffId">mã người dùng</param>
    /// <param name="fileId">mã file đăng ký</param>
    /// <returns></returns>
    Task<StaffAttackManager> GetByFileId(Guid staffId, Guid fileId);
}