using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.CompanyManagers;

namespace ITC.Domain.Interfaces.CompanyManagers.StaffManagers;

/// <summary>
///     Lớp interface repository nhân viên
/// </summary>
public interface IStaffManagerRepository : IRepository<StaffManager>
{
    /// <summary>
    ///     Trả về StaffManager theo mã người dùng
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StaffManager> GetByUserId(string id);

    Task<List<Guid>> GetByOwnerIdAsync(Guid ownerId);
}