using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Models.StudyManagers;

namespace ITC.Domain.Interfaces.StudyManagers.SubjectManagers;

/// <summary>
///     Lớp interface repository danh sách môn học
/// </summary>
public interface ISubjectManagerRepository : IRepository<SubjectManager>
{
    /// <summary>
    ///     Xử lý left - right
    /// </summary>
    /// <param name="model"></param>
    /// <param name="left"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    int DeQuyLeftRight(List<SubjectManager> model, int left, string parentId);

    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <param name="typeId">Loại dữ liệu</param>
    /// <param name="projectId">Mã dự án</param>
    /// <returns></returns>
    Task<int> GetMaxPosition(int typeId, Guid projectId);

    /// <summary>
    ///     Danh sách ID theo điều kiê Left - Right
    /// </summary>
    /// <param name="vLeft">Giá trị trái</param>
    /// <param name="vRight">Giá trị phải</param>
    /// <returns></returns>
    Task<List<Guid>> GetListIdFromLeftRight(int vLeft, int vRight);

    /// <summary>
    ///     Lấy dữ liệu theo SecrectKey
    /// </summary>
    /// <param name="secrectKey"></param>
    /// <returns></returns>
    Task<SubjectManager> GetBySecrectKey(string secrectKey);
}