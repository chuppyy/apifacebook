using System.Threading.Tasks;

namespace ITC.Domain.Interfaces.StudyManagers.MinusWord;

/// <summary>
///     Lớp interface repository loại môn học
/// </summary>
public interface IMinusWordRepository : IRepository<Models.MenuManager.MinusWord>
{
    /// <summary>
    ///     Trả về vị trí lớn nhất
    /// </summary>
    /// <returns></returns>
    Task<int> GetMaxPosition();
}