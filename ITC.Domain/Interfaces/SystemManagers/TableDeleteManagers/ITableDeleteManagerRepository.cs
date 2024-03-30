using System.Threading.Tasks;
using ITC.Domain.Models.SystemManagers;

namespace ITC.Domain.Interfaces.SystemManagers.TableDeleteManagers;

/// <summary>
///     Class interface repository danh sách các table cần xóa
/// </summary>
public interface ITableDeleteManagerRepository : IRepository<TableDeleteManager>
{
    /// <summary>
    ///     Trả về tên table theo mã code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<string> GetTableNameByCode(int code);
}