#region

using System.Collections.Generic;
using System.Threading.Tasks;
using NCore.Modals;

#endregion

namespace ITC.Domain.Interfaces.NewsManagers.NewsVercelManagers;

/// <summary>
///     Lớp interface query loại nhóm tin
/// </summary>
public interface INewsVercelQueries
{
    /// <summary>
    /// Trả về 1 dữ liệu domain
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> ListVercel();
}