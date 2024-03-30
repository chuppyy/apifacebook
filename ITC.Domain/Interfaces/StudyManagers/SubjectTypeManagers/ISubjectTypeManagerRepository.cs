using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.StudyManagers;

namespace ITC.Domain.Interfaces.StudyManagers.SubjectTypeManagers;

/// <summary>
///     Lớp interface repository loại môn học
/// </summary>
public interface ISubjectTypeManagerRepository : IRepository<SubjectTypeManager>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();

    /// <summary>
    ///     Trả về danh sách ID của dữ liệu theo groupTable
    /// </summary>
    /// <param name="groupTable"></param>
    /// <returns></returns>
    Task<List<Guid>> GetListIdByGroupTable(int groupTable);
}